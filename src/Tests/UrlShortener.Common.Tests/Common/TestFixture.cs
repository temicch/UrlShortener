using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces;
using UrlShortener.Infrastructure;

namespace UrlShortener.Common.Tests.Common
{
    public class TestFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
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

                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlite("DataSource=test_db.db")
                    );

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var dbContext = (DbContext)scope.ServiceProvider
                        .GetRequiredService<IDbContext>();

                    try
                    {
                        dbContext.Database.Migrate();
                    }
                    catch (Exception)
                    {
                    }
                });
        }

        protected override void Dispose(bool disposing)
        {
            using (var scope = Server.Services.CreateScope())
            {
                var dbContext = (DbContext)scope.ServiceProvider
                    .GetRequiredService<IDbContext>();
                try
                {
                    dbContext.Database.EnsureDeleted();
                }
                catch (Exception)
                {
                }
            }

            base.Dispose(disposing);
        }
    }
}