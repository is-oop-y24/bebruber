using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.Services;

public interface IDriverLocationService
{
    Task<IReadOnlyCollection<Driver>> GetDriversNearbyAsync(Coordinate coordinate, CancellationToken cancellationToken);
    Task UpdateDriverLocationAsync(Driver driver, Coordinate coordinate, CancellationToken cancellationToken);

    Task SubscribeToLocationUpdatesAsync(Driver driver, Client client, CancellationToken cancellationToken);
    Task UnsubscribeFromLocationUpdatedAsync(Driver driver, Client client, CancellationToken cancellationToken);
}