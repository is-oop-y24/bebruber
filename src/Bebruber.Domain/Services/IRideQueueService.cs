using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;

namespace Bebruber.Domain.Services;

public interface IRideQueueService
{
    Task EnqueueRideEntryAsync(RideEntry rideEntry, CancellationToken cancellationToken);
    Task<RideEntry> DequeueRideEntryAsync(Guid entryId, CancellationToken cancellationToken);
}