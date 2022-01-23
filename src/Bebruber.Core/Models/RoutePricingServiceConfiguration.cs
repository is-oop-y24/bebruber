namespace Bebruber.Core.Models;

public record RoutePricingServiceConfiguration(
    decimal LandingPrice,
    decimal PricePerKilometer,
    decimal LoadMultiplier);