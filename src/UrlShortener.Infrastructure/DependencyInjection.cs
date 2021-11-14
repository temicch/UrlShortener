using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Interfaces.Services;

namespace UrlShortener.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    ///     Add services for <c>Infrastructure layer</c> working
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration Configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
                Configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IDbContext, ApplicationDbContext>();
        services.AddTransient<IDateTimeService, DateTimeService>();

        return services;
    }
}
