using System;
using System.Threading.Tasks;
using Bebruber.Domain.Models;

namespace Bebruber.Domain.Services;

public interface IRideQueueService
{
    Task EnqueueRideEntry(RideEntry rideEntry);
    Task<RideEntry> Dequeue(Guid entryId);
}