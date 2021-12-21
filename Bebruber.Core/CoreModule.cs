using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bebruber.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CoreModule).Assembly);

            return services;
        }
    }
}