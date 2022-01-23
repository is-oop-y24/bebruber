using Bebruber.Core.Models;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Core.Services;

public class RoutePricingService : IPricingService
{
    private RoutePricingServiceConfiguration _configuration;

    public RoutePricingService(RoutePricingServiceConfiguration pricingServiceConfiguration)
    {
        _configuration = pricingServiceConfiguration;
    }

    public Roubles Calculate(RideContext context)
    {
        var finalPrice = new Roubles(0);

        return context.Route.Select(
            item => new Roubles(
                (decimal)item.Length *
                (decimal)item.LoadLevel *
                _configuration.PricePerKilometer *
                _configuration.LoadMultiplier))
            .Aggregate(finalPrice, (current, delta) => current + delta);
    }
}