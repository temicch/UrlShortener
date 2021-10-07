using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UrlShortener.Application.Implementation.Common.Behaviours
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex) when (!(ex is ValidationException))
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName,
                    request);

                throw;
            }
        }
    }
}