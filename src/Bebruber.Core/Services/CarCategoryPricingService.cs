using Bebruber.Core.Models;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Core.Services;

public class CarCategoryPricingService : IPricingService
{
    private readonly ICarCategoryPricingServiceConfiguration _configuration;
    private readonly IPricingService _routePricingService;

    public CarCategoryPricingService(
        IPricingService routePricingService,
        ICarCategoryPricingServiceConfiguration configuration)
    {
        _configuration = configuration;
        _routePricingService = routePricingService;
    }

    public Roubles Calculate(RideContext context)
    {
        return _routePricingService.Calculate(context) *
               _configuration.CalculatePriceMultiplier(context.Driver.Car.Category);
    }
}