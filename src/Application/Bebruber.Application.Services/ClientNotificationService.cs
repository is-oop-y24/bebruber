using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Services;

public class ClientNotificationService : IClientNotificationService
{
    public Task PostDriverCoordinatesAsync(Client client, Coordinate coordinate, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task NotifyDriverFoundAsync(Client client, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task NotifyDriverArrivedAsync(Client client, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task NotifyRideStartedAsync(Client client, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task NotifyRideFinishedAsync(Client client, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}