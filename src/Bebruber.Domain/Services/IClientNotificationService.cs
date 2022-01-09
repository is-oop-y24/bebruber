using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.Services;

public interface IClientNotificationService
{
    Task PostDriverCoordinatesAsync(Client client, Coordinate coordinate, CancellationToken cancellationToken);
    Task NotifyDriverFoundAsync(Client client, CancellationToken cancellationToken);
    Task NotifyDriverArrivedAsync(Client client, CancellationToken cancellationToken);
    Task NotifyRideFinishedAsync(Client client, CancellationToken cancellationToken);
}