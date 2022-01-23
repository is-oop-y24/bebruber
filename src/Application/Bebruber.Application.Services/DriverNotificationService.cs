using Bebruber.Application.Common.Extensions;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Endpoints.SignalR.Drivers;
using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Application.Services;

public class DriverNotificationService : IDriverNotificationService
{
    private readonly IHubContext<DriverHub, IDriverClient> _hub;

    public DriverNotificationService(IHubContext<DriverHub, IDriverClient> hub)
    {
        _hub = hub;
    }

    public Task OfferRideToDriverAsync(
        Driver driver, RideEntry rideEntry, TimeSpan awaitingTimeSpan, CancellationToken cancellationToken)
        => _hub.Clients.User(driver.Id.ToString()).OfferRideToDriver(rideEntry.ToDto(), awaitingTimeSpan);

    public Task NotifySuccessfulAcceptanceAsync(
        Driver driver, Ride rideEntry, CancellationToken cancellationToken)
        => _hub.Clients.User(driver.Id.ToString()).NotifySuccessfulAcceptance(rideEntry.ToDto());

    public Task NotifyFailedAcceptanceAsync(Driver driver, CancellationToken cancellationToken)
        => _hub.Clients.User(driver.Id.ToString()).NotifyFailedAcceptance();
}