using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;

namespace Bebruber.Application.Services;

public class RideService : IRideService
{
    public Task<Ride> RegisterRideAsync(RideContext context, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task SetRideDriverArrivedAsync(Ride ride, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task StartRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task FinishRideAsync(Ride ride, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}