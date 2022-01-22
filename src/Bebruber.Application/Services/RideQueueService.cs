using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Application.Services.Exceptions;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Utility.Extensions;
using FluentResults;

namespace Bebruber.Application.Services;

public class RideQueueService : IRideQueueService
{
    private readonly RideEntryDatabaseContext _context;

    public RideQueueService(RideEntryDatabaseContext context)
    {
        _context = context.ThrowIfNull();
    }

    public async Task<Result> EnqueueRideEntryAsync(RideEntry rideEntry, CancellationToken cancellationToken)
    {
        RideEntry? existingEntry = await _context.Entries
            .FindAsync(new object?[] { rideEntry.Id }, cancellationToken);

        if (existingEntry is not null)
            throw new RideEntryEnqueuedException(rideEntry);

        await _context.Entries.AddAsync(rideEntry, cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<RideEntry>> DequeueRideEntryAsync(Guid entryId, CancellationToken cancellationToken)
    {
        RideEntry? existingEntry = await _context.Entries
            .FindAsync(new object?[] { entryId }, cancellationToken);

        if (existingEntry is null)
            return Result.Fail(new Error($"{existingEntry?.ToString()} is null"));

        _context.Entries.Remove(existingEntry);
        return Result.Ok(existingEntry);
    }
}