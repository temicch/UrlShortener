using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Infrastructure;

namespace UrlShortener.Common.Tests.Common;

public class TestFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected DbConnection _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                _connection = new SqliteConnection("datasource=:memory:");
                _connection.Open();

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(_connection)
                );

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            });
    }

    public override async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await base.DisposeAsync();
    }
}