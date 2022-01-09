using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Models;

namespace Bebruber.Domain.Services;

public interface IRideQueueService
{
    Task EnqueueRideEntryAsync(RideEntry rideEntry, CancellationToken cancellationToken);
    Task<RideEntry> DequeueAsync(Guid entryId, CancellationToken cancellationToken);
}