using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UrlShortener.Infrastructure;

namespace UrlShortener.WebUI.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> MigrateDatabaseAsync(this IHost host,
        CancellationToken cancellationToken = default)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlite())
                await context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();

            logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            throw;
        }

        return host;
    }
}
