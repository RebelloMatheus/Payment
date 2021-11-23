using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Payment.Application.WebApi
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
            services.AddControllers();
            services.AddServicesApi(Configuration);
            AddServicesSwagger(services);
        }

        private static void AddServicesSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Assessment Challenge",
                    Version = "beta",
                    Description = "Conceptual API of a challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Rebello",
                        Email = "matheus_rebello@yahoo.com.br",
                        Url = new Uri("https://www.linkedin.com/in/matheus-rebello-4a37a5b6/")
                    },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UseSwagger(app);
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(x =>
            {
                x.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty;
                x.SwaggerEndpoint("/swagger/v2/swagger.json", "Interface default swagger");
            });
        }
    }
}