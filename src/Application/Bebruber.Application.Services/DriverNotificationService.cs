using Bebruber.Application.Common.Extensions;
using Bebruber.Application.Requests.Drivers.Queries;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Endpoints.SignalR.Drivers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Application.Services;

public class DriverNotificationService : IDriverNotificationService
{
    private readonly IHubContext<DriverHub, IDriverClient> _hub;
    private readonly IMediator _mediator;

    public DriverNotificationService(IHubContext<DriverHub, IDriverClient> hub, IMediator mediator)
    {
        _hub = hub;
        _mediator = mediator;
    }

    public async Task OfferRideToDriverAsync(
        Driver driver, RideEntry rideEntry, TimeSpan awaitingTimeSpan, CancellationToken cancellationToken)
    {
        string userId = await GetDriverUserIdAsync(driver, cancellationToken);
        await _hub.Clients.User(userId).OfferRideToDriverAsync(rideEntry.ToDto(), awaitingTimeSpan);
    }

    public async Task NotifySuccessfulAcceptanceAsync(
        Driver driver, Ride rideEntry, CancellationToken cancellationToken)
    {
        string userId = await GetDriverUserIdAsync(driver, cancellationToken);
        await _hub.Clients.User(userId).NotifySuccessfulAcceptanceAsync(rideEntry.ToDto());
    }

    public async Task NotifyFailedAcceptanceAsync(Driver driver, CancellationToken cancellationToken)
    {
        string userId = await GetDriverUserIdAsync(driver, cancellationToken);
        await _hub.Clients.User(userId).NotifyFailedAcceptanceAsync();
    }

    private async Task<string> GetDriverUserIdAsync(Driver driver, CancellationToken cancellationToken)
    {
        var query = new GetDriverUserId.Query(driver.Id);
        GetDriverUserId.Response response = await _mediator.Send(query, cancellationToken);
        return response.UserId;
    }
}