using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UrlShortener.Application.UseCases;

public static class DependencyInjection
{
    /// <summary>
    ///     Add services for <c>UseCases layer</c> working
    /// </summary>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}