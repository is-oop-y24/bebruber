using Bebruber.Application.Services.Exceptions;
using Bebruber.Application.Services.Models;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Utility.Extensions;
using FluentResults;

namespace Bebruber.Application.Services;

public class RideQueueService : IRideQueueService
{
    private readonly BebruberDatabaseContext _context;
    private readonly IDriverLocationService _locationService;
    private readonly IDriverNotificationService _driverNotificationService;
    private readonly RideQueueServiceConfiguration _configuration;

    public RideQueueService(
        IDriverLocationService locationService,
        IDriverNotificationService driverNotificationService,
        RideQueueServiceConfiguration configuration,
        BebruberDatabaseContext context)
    {
        _context = context;
        _driverNotificationService = driverNotificationService.ThrowIfNull();
        _configuration = configuration.ThrowIfNull();
        _locationService = locationService.ThrowIfNull();
    }

    public async Task EnqueueRideEntryAsync(RideEntry rideEntry, CancellationToken cancellationToken)
    {
        RideEntry? existingEntry = await _context.Entries
            .FindAsync(new object?[] { rideEntry.Id }, cancellationToken);

        if (existingEntry is not null)
            throw new RideEntryEnqueuedException(rideEntry);

        // Needs to run in background
#pragma warning disable CS4014
        FindDriverForRideEntryAsync(rideEntry, cancellationToken);
#pragma warning restore CS4014
        await _context.Entries.AddAsync(rideEntry, cancellationToken);
    }

    public async Task<Result<RideEntry>> DequeueRideEntryAsync(Guid entryId, CancellationToken cancellationToken)
    {
        RideEntry? existingEntry = await _context.Entries
            .FindAsync(new object?[] { entryId }, cancellationToken);

        if (existingEntry is null)
            return Result.Fail(new Error($"{nameof(existingEntry)} is null"));

        _context.Entries.Remove(existingEntry);
        await _context.SaveChangesAsync(cancellationToken);

        existingEntry.State = RideEntryState.Dequeued;
        return Result.Ok(existingEntry);
    }

    public async Task<Result> DismissRideEntryAsync(Guid entryId, Driver driver, CancellationToken cancellationToken)
    {
        RideEntry? existingEntry = await _context.Entries
            .FindAsync(new object?[] { entryId }, cancellationToken);

        if (existingEntry is null)
            return Result.Fail(new Error($"{nameof(existingEntry)} is null"));

        existingEntry.Dismiss(driver);
        return Result.Ok();
    }

    private async Task FindDriverForRideEntryAsync(RideEntry entry, CancellationToken cancellationToken)
    {
        while (entry.State is RideEntryState.Enqueued)
        {
            // TODO: Search radius extension
            IReadOnlyCollection<Driver> drivers = await _locationService
                .GetDriversNearbyAsync(entry.Origin.Coordinate, cancellationToken);

            drivers = drivers.Except(entry.DismissedDrivers).ToList();

            Task[] tasks = drivers
                .Select(d => _driverNotificationService.OfferRideToDriverAsync(
                    d, entry, _configuration.AwaitingTimeSpan, cancellationToken))
                .ToArray();

            await Task.WhenAll(tasks);
            await Task.Delay(_configuration.AwaitingTimeSpan, cancellationToken);
        }
    }
}