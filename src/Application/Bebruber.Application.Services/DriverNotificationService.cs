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

    public async Task OfferRideToDriverAsync(
        Driver driver, RideEntry rideEntry, TimeSpan awaitingTimeSpan, CancellationToken cancellationToken)
    {
        await _hub.Clients.User(driver.Id.ToString()).OfferRideToDriverAsync(rideEntry.ToDto(), awaitingTimeSpan);
    }

    public async Task NotifySuccessfulAcceptanceAsync(
        Driver driver, Ride rideEntry, CancellationToken cancellationToken)
    {
        await _hub.Clients.User(driver.Id.ToString()).NotifySuccessfulAcceptanceAsync(rideEntry.ToDto());
    }

    public async Task NotifyFailedAcceptanceAsync(Driver driver, CancellationToken cancellationToken)
    {
        await _hub.Clients.User(driver.Id.ToString()).NotifyFailedAcceptanceAsync();
    }
}