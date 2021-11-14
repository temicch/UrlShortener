using Microsoft.AspNetCore.Builder;
using UrlShortener.WebUI;
using UrlShortener.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();
await app.MigrateDatabaseAsync();

startup.Configure(app, app.Environment);

app.Run();