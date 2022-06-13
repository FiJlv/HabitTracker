using HabitTracker.Application.Common.Mappings;
using HabitTracker.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HabitTracker.Application;
using HabitTracker.Persistence;
using Microsoft.Extensions.Configuration;
using HabitTracker.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HabitTracker.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IHabitTrackerDbContext).Assembly)); 
            });

            services.AddApplication();
            services.AddPersistance(Configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme =
                      JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:44342/";
                    options.Audience = "HabitTrakerWebAPI";
                    options.RequireHttpsMetadata = false;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
