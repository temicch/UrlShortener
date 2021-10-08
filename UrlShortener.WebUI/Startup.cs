using System;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UrlShortener.Application.Implementation;
using UrlShortener.Infrastructure;
using UrlShortener.WebUI.Middleware;

namespace UrlShortener.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddApplication();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers()
                .AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "SwaggerHelper.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddFluentValidationRulesToSwagger();

            services.AddWebOptimizer(pipeline => { pipeline.CompileScssFiles(null, "css/**/*.*css"); });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddAntDesign();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "My API V1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseMiddleware<ApiErrorWrappingMiddleware>();

            app.UseHttpsRedirection();
            app.UseWebOptimizer();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}