using Bebruber.Core.Models;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Core.Services;

public class RoutePricingService : IPricingService
{
    private readonly RoutePricingServiceConfiguration _configuration;

    public RoutePricingService(RoutePricingServiceConfiguration pricingServiceConfiguration)
    {
        _configuration = pricingServiceConfiguration;
    }

    public Roubles Calculate(RideContext context)
    {
        return context.Route.Aggregate(
            new Roubles(0),
            (i, sector) => i + new Roubles(Calculate(sector)));
    }

    private decimal Calculate(RouteSector sector)
    {
        return (decimal)sector.Length *
               (decimal)sector.LoadLevel *
               _configuration.PricePerKilometer *
               _configuration.LoadMultiplier;
    }
}