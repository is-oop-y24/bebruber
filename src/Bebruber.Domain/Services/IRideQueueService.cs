using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using FluentResults;

namespace Bebruber.Domain.Services;

public interface IRideQueueService
{
    Task EnqueueRideEntryAsync(RideEntry rideEntry, CancellationToken cancellationToken);
    Task<Result<RideEntry>> DequeueRideEntryAsync(Guid entryId, CancellationToken cancellationToken);
    Task<Result> DismissRideEntryAsync(Guid entryId, Driver driver, CancellationToken cancellationToken);
}