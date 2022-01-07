using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.Services;

public interface IClientNotificationService
{
    Task PostDriverCoordinatesAsync(Client client, Coordinate coordinate, CancellationToken cancellationToken);
}