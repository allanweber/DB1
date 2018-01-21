using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RH.Domain.Constants;
using RH.Domain.Core.Repositories;
using RH.Domain.Repositories;
using RH.Domain.Services;
using RH.Infrastructure.Repositories;
using RH.Infrastructure.Services;
using RH.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace RH
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrincipalDbContext>(options =>
            {
                if (this.Environment.IsEnvironment("IntegrationTests"))
                {
                    options.UseInMemoryDatabase("IntegrationTests");
                }
                else
                {
                    options.UseMySQL(this.Configuration.GetConnectionString("RelationalConnection"));
                }
            });

            services.AddMvc();

            services.AddAutoMapper();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                new Info
                {
                    Title = "RH",
                    Version = "v1",
                    Description = "RH App",
                    Contact = new Contact
                    {
                        Name = "Allan Cassiano Weber",
                        Url = "https:/github.com/allanweber"
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "RH.xml");
                s.IncludeXmlComments(xmlPath);
            });

            services.AddCors(o => o.AddPolicy(AppConstants.ALLOWALLHEADERS, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITechnologyRepository, TechnologyRepository>();
            services.AddScoped<ITechnologyService, TechnologyService>();
            services.AddScoped<IOpportunityRepository, OpportunityRepository>();
            services.AddScoped<IOpportunityService, OpportunityService>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICandidateTechRepository, CandidateTechRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvcWithDefaultRoute();

            app.UseCors(AppConstants.ALLOWALLHEADERS);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "RH");
            });
        }
    }
}
