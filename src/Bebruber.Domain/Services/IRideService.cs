using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;

namespace Bebruber.Domain.Services;

public interface IRideService
{
    Task<Ride> RegisterRideAsync(RideContext context, CancellationToken cancellationToken);
    Task SetRideDriverArrivedAsync(Ride ride, CancellationToken cancellationToken);
    Task StartRideAsync(Ride ride, CancellationToken cancellationToken);
    Task FinishRideAsync(Ride ride, CancellationToken cancellationToken);
}