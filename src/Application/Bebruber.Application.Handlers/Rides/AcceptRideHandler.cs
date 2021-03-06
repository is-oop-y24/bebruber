using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Utility.Extensions;
using FluentResults;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.AcceptRide.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.AcceptRide.Response;

namespace Bebruber.Application.Handlers.Rides;

public class AcceptRideHandler : IRequestHandler<Command, Response>
{
    private readonly BebruberDatabaseContext _databaseContext;
    private readonly IRideQueueService _rideQueueService;
    private readonly IRideService _rideService;
    private readonly IRouteService _routeService;
    private readonly IDriverLocationService _driverLocationService;

    public AcceptRideHandler(
        IRideQueueService rideQueueService,
        IRideService rideService,
        BebruberDatabaseContext databaseContext,
        IRouteService routeService,
        IDriverLocationService driverLocationService)
    {
        _rideQueueService = rideQueueService;
        _rideService = rideService;
        _databaseContext = databaseContext;
        _routeService = routeService;
        _driverLocationService = driverLocationService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        (Guid rideEntryId, Guid clientId, Guid driverId) = request;

        Result<RideEntry> result = await _rideQueueService
            .DequeueRideEntryAsync(rideEntryId, cancellationToken);

        if (result.IsFailed)
            return new Response(Guid.Empty);

        Driver? driver = await _databaseContext.Drivers.FindAsync(new object?[] { driverId }, cancellationToken);
        Client? client = await _databaseContext.Clients.FindAsync(new object?[] { clientId }, cancellationToken);

        driver = driver.ThrowIfNull();
        client = client.ThrowIfNull();

        RideEntry rideEntry = result.Value;
        Route route = await _routeService
            .CreateRouteAsync(rideEntry.Origin, rideEntry.Destination, rideEntry.IntermediatePoints);

        var rideContext = new RideContext(
            client,
            driver,
            route,
            rideEntry.Origin,
            rideEntry.Destination,
            rideEntry.IntermediatePoints);

        Ride ride = await _rideService.RegisterRideAsync(rideContext, cancellationToken);
        await _driverLocationService.SubscribeToLocationUpdatesAsync(driver, client, cancellationToken);
        return new Response(ride.Id);
    }
}