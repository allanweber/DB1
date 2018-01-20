using AutoMapper;
using Git.Domain.Constants;
using Git.Domain.Services;
using Git.Filters;
using Git.Infrastructure.Services;
using Git.Infrastructure.Services.Facades;
using Git.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Git
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
            services.AddMvc()
                .AddMvcOptions(setup => setup.Filters.Add<NotificationFilterAttribute>());
            services.AddAutoMapper();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                new Info
                {
                    Title = "Git",
                    Version = "v1",
                    Description = "Cosumo de apis do GitHunb",
                    Contact = new Contact
                    {
                        Name = "Allan Cassiano Weber",
                        Url = "https://github.com/allanweber"
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "Git.xml");
                s.IncludeXmlComments(xmlPath);
            });

            services.AddCors(o => o.AddPolicy(AppConstants.ALLOWALLHEADERS, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped<IGitUserService, GitUserService>();
            services.AddScoped<IGitRepositoryService, GitRepositoryService>();
            services.AddScoped<GitHubFacadeService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseExceptionHandler(configure => GlobalExceptionHandlerMiddleware.Handle(configure, env));

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvcWithDefaultRoute();

            app.UseCors(AppConstants.ALLOWALLHEADERS);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Git");
            });
        }
    }
}
