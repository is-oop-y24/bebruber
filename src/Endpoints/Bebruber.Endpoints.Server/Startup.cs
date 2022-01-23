using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Bebruber.Application;
using Bebruber.Application.Models;
using Bebruber.Application.Services;
using Bebruber.DataAccess;
using Bebruber.DataAccess.Seeding;
using Bebruber.Domain.Services;
using Bebruber.Identity;
using Bebruber.Identity.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
            services.AddScoped<IPricingService, PricingService>();
            services.AddScoped<IRideQueueService, RideQueueService>();
            services.AddScoped<IRideService, RideService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ITimeProviderService, TimeProviderService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddControllers();
            services.AddSignalR();
            services.AddCoreModule();
            services.AddSwaggerGen(c =>
                                   {
                                       c.CustomSchemaIds(type => type.FullName);
                                       c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bebruber.Endpoints.Server", Version = "v1" });
                                   });

            services.AddDbContext<BebruberDatabaseContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddDbContext<DriverLocationDatabaseContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddDbContext<RideEntryDatabaseContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            //TODO: change
            services.AddSingleton(new DriverLocationServiceConfiguration(10, TimeSpan.Zero));
            services.AddSingleton(new RideQueueServiceConfiguration(TimeSpan.Zero));

            services.AddDbContext<IdentityDatabaseContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddScoped<IdentityDatabaseSeeder>();

            var builder = services.AddIdentity<IdentityUser, IdentityRole>(m =>
                                                                           {
                                                                               m.Password.RequireDigit = false;
                                                                               m.Password.RequiredLength = 0;
                                                                               m.Password.RequireLowercase = false;
                                                                               m.Password.RequireUppercase = false;
                                                                               m.Password.RequiredUniqueChars = 0;
                                                                               m.Password.RequireNonAlphanumeric = false;
                                                                           })
                                  .AddEntityFrameworkStores<IdentityDatabaseContext>()
                                  .AddSignInManager<SignInManager<IdentityUser>>()
                                  .AddDefaultTokenProviders();
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