using Bebruber.Application.Common.Extensions;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Endpoints.SignalR.Users;
using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Application.Services;

public class ClientNotificationService : IClientNotificationService
{
    private readonly IHubContext<UserHub, IUserClient> _hub;

    public ClientNotificationService(IHubContext<UserHub, IUserClient> hub)
    {
        _hub = hub;
    }

    public Task PostDriverCoordinatesAsync(Client client, Coordinate coordinate, CancellationToken cancellationToken)
        => _hub.Clients.User(client.Email.Value).PostDriverCoordinates(coordinate.ToDto());

    public Task NotifyDriverFoundAsync(Client client, CancellationToken cancellationToken)
        => _hub.Clients.User(client.Email.Value).NotifyDriverFound();

    public Task NotifyDriverArrivedAsync(Client client, CancellationToken cancellationToken)
        => _hub.Clients.User(client.Email.Value).NotifyDriverArrived();

    public Task NotifyRideStartedAsync(Client client, CancellationToken cancellationToken)
        => _hub.Clients.User(client.Email.Value).NotifyRideStarted();

    public Task NotifyRideFinishedAsync(Client client, CancellationToken cancellationToken)
        => _hub.Clients.User(client.Email.Value).NotifyRideFinished();
}