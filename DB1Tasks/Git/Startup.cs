using AutoMapper;
using Git.Domain.Constants;
using Git.Domain.Services;
using Git.Infrastructure.Services;
using Git.Infrastructure.Services.Facades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc();
            services.AddAutoMapper();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseCors(AppConstants.ALLOWALLHEADERS);
        }
    }
}
