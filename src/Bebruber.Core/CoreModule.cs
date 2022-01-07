using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bebruber.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CoreModule).Assembly);
            AssemblyScanner.FindValidatorsInAssembly(typeof(CoreModule).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

            return services;
        }
    }
}