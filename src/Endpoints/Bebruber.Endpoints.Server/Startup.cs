using System;
using Bebruber.Application.Common;
using Bebruber.Application.Common.Behaviours;
using Bebruber.Application.Requests;
using Bebruber.Application.Services;
using Bebruber.Application.Services.Models;
using Bebruber.Core.Services;
using Bebruber.DataAccess;
using Bebruber.DataAccess.Seeding;
using Bebruber.Domain.Services;
using Bebruber.Identity;
using Bebruber.Identity.Tools;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Bebruber.Endpoints.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IClientNotificationService, ClientNotificationService>();
            services.AddScoped<IDriverLocationService, DriverLocationService>();
            services.AddScoped<IDriverNotificationService, DriverNotificationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IRideQueueService, RideQueueService>();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ITimeProviderService, TimeProviderService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPricingService, RoutePricingService>();

            services.Decorate<IPricingService, CarCategoryPricingService>();

            services.AddControllers();
            services.AddSignalR();

            services.AddMediatR(typeof(Application.Handlers.IAssemblyMarker).Assembly);
            AssemblyScanner.FindValidatorsInAssembly(typeof(Application.Handlers.IAssemblyMarker).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehavior<,>));

            services.AddSwaggerGen(
                c =>
                {
                    c.CustomSchemaIds(type => type.FullName);

                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo { Title = "Bebruber.Endpoints.Server", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "JSON Web Token to access resources. Example: Bearer {token}",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            },
                            new[] { string.Empty }
                        },
                    });
                });

            services.AddDbContext<BebruberDatabaseContext>(
                opt =>
                {
                    opt.UseSqlite("Filename=BebruberDatabase.db");
                    opt.UseLazyLoadingProxies();
                });

            // TODO: change
            services.AddSingleton(new DriverLocationServiceConfiguration(10, TimeSpan.Zero));
            services.AddSingleton(new RideQueueServiceConfiguration(TimeSpan.Zero));

            services.AddDbContext<IdentityDatabaseContext>(opt => opt.UseSqlite("Filename=identity.db"));
            services.AddScoped<IdentityDatabaseSeeder>();

            services.AddIdentity<ApplicationUser, IdentityRole>(m =>
                                                             {
                                                                 m.Password.RequireDigit = false;
                                                                 m.Password.RequiredLength = 0;
                                                                 m.Password.RequireLowercase = false;
                                                                 m.Password.RequireUppercase = false;
                                                                 m.Password.RequiredUniqueChars = 0;
                                                                 m.Password.RequireNonAlphanumeric = false;
                                                             })
                    .AddEntityFrameworkStores<IdentityDatabaseContext>()
                    .AddSignInManager<SignInManager<ApplicationUser>>();

            var signingConfigurations = new SigningConfigurations(Configuration["TokenKey"]);
            services.AddSingleton(signingConfigurations);

            var tokenOptions = new JwtTokenOptions()
            {
                Audience = "SampleAudience",
                Issuer = "Bebruber",
            };

            services.AddSingleton(tokenOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.SecurityKey,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bebruber.Endpoints.Server v1"));
            }

            app.UseHttpsRedirection();
            app.UseHttpLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}