using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using UrlShortener.WebUI.Extensions;

namespace UrlShortener.WebUI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger,
        ProblemDetailsFactory problemDetailsFactory)
    {
        _next = next;
        _logger = logger;
        _problemDetailsFactory = problemDetailsFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception) when (!context.Response.HasStarted)
        {
            var code = HttpStatusCode.InternalServerError;
            object result = null;

            switch (exception)
            {
                case ValidationException ex:
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelErrors(ex);
                    code = HttpStatusCode.BadRequest;
                    result = _problemDetailsFactory
                        .CreateValidationProblemDetails(context, modelState);
                    break;
                default:
                    _logger.LogError(exception, exception.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
