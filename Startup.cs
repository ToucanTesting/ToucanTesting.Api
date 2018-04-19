using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ToucanTesting.Data;
using ToucanTesting.Filters;

namespace ToucanTesting
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll", p => p
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()))
            .AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(typeof(ValidateModelAttribute));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddScoped<ITestSuiteRepository, TestSuiteRepository>();
            services.AddScoped<ITestRunRepository, TestRunRepository>();
            services.AddScoped<ITestResultRepository, TestResultRepository>();
            services.AddScoped<ITestModuleRepository, TestModuleRepository>();
            services.AddScoped<ITestCaseRepository, TestCaseRepository>();
            services.AddScoped<ITestActionRepository, TestActionRepository>();
            services.AddScoped<ITestConditionRepository, TestConditionRepository>();
            services.AddScoped<IExpectedResultRepository, ExpectedResultRepository>();
            services.AddScoped<ITestIssueRepository, TestIssueRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();
            services.AddDbContext<ToucanDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://toucantesting.auth0.com/";
                options.Audience = "http://api.toucantesting.com";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseAuthentication();

            if (env.IsDevelopment() || env.IsEnvironment("Ubuntu"))
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
