using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using EasyCronJob.Core;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PublicApi.Util.Authorization;
using PublicApi.Util.Middleware;
using System;
using System.Collections.Generic;

namespace PublicApi.Util
{
    public class Startup
    {
        private const string CORS_POLICY = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureInMemoryDatabases(services);

            // use real database
            ConfigureProductionServices(services);
        }

        public void ConfigureDockerServices(IServiceCollection services)
        {
            ConfigureDevelopmentServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            services.AddDbContext<Infrastructure.Data.AppContext>(c =>
                c.UseInMemoryDatabase("Catalog"));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<Infrastructure.Data.AppContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Infrastructure.Data.AppContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddTransient<IFoodProductService, FoodProductService>();
            services.AddTransient<IFoodStockService, FoodStockService>();
            services.AddTransient<IMealService, MealService>();
            services.AddTransient<IDietPlanService, DietPlanService>();
            services.AddTransient<IDietPlanPeriodService, DietPlanPeriodService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INotificationRuleService, NotificationRuleService>();
            services.AddTransient<IUserWeightEvidentionService, UserWeightEvidentionService>();
            services.AddTransient<IUserSubscriptionService, UserSubscriptionService>();
            services.AddSingleton<IActiveUsersSingleton, ActiveUsersSingleton>();
            services.AddSingleton<IAlertService, AlertService>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IAuthorizationHandler, AuthorizedUserHandler>();
            services.ApplyResulation<AlertServiceCronJob>(options => {
                options.CronExpression = "* * * * *";
                options.TimeZoneInfo = TimeZoneInfo.Local;
            });

            var baseUrlConfig = new BaseUrlConfiguration();
            Configuration.Bind(BaseUrlConfiguration.CONFIG_NAME, baseUrlConfig);

            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                // TODO: Put this in config
                options.Authority = Configuration.GetConnectionString("IdentityAuthority");
                // TODO: KILL THIS
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidIssuers = Configuration["Issuer"].Split(',');
                options.TokenValidationParameters.ValidAudiences = new[] {"api1"};
                options.TokenValidationParameters.ValidTypes = new[] {"at+jwt"};
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyUtil.AuthorizeUserIdPolicy, policy =>
                    policy.Requirements.Add(new AuthorizationUserRequirement()));
            });


            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY,
                    builder =>
                    {
                        builder.WithOrigins(baseUrlConfig.WebBase.Replace("host.docker.internal", "localhost")
                            .TrimEnd('/'));
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.EnableAnnotations();
                c.SchemaFilter<CustomSchemaFilters>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CORS_POLICY);
            app.UseAuthentication();

            // Adds active users Id's to a singleton class
            app.UseMiddleware<ActiveUsersMiddleWare>();
            app.UseAuthorization();



            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}