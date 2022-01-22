using Bebruber.Domain.Models;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.Services;

public interface IPricingService
{
    Roubles Calculate(RideContext context);
}