using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;

namespace Bebruber.Application.Services;

public class DriverNotificationService : IDriverNotificationService
{
    public Task OfferRideToDriverAsync(Driver driver, RideEntry rideEntry, TimeSpan awaitingTimeSpan,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task NotifySuccessfulAcceptanceAsync(Driver driver, Ride rideEntry, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task NotifyFailedAcceptanceAsync(Driver driver, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}