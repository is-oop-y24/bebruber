using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;

namespace Bebruber.Domain.Services;

public interface IDriverNotificationService
{
    Task OfferRideToDriverAsync(Driver driver, RideEntry rideEntry, TimeSpan awaitingTimeSpan, CancellationToken cancellationToken);
    Task NotifySuccessfulAcceptanceAsync(Driver driver, RideEntry rideEntry, CancellationToken cancellationToken);
    Task NotifyFailedAcceptanceAsync(Driver driver, CancellationToken cancellationToken);
}