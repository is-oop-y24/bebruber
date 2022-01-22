using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Utility.Extensions;
using FluentResults;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public static class AcceptRideCommand
{
    public record Command(Guid RideEntryId, Guid ClientId, Guid DriverId) : IRequest<Response>;

    public record Response(Guid RideId);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly BebruberDatabaseContext _databaseContext;
        private readonly IRideQueueService _rideQueueService;
        private readonly IRideService _rideService;
        private readonly IRouteService _routeService;

        CommandHandler(
            IRideQueueService rideQueueService,
            IRideService rideService,
            BebruberDatabaseContext databaseContext,
            IRouteService routeService)
        {
            _rideQueueService = rideQueueService;
            _rideService = rideService;
            _databaseContext = databaseContext;
            _routeService = routeService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            Result<RideEntry> result = await _rideQueueService
                .DequeueRideEntryAsync(request.RideEntryId, cancellationToken);

            if (result.IsFailed)
                return new Response(Guid.Empty);

            Driver? driver = await _databaseContext.Drivers
                .FindAsync(new object?[] { request.DriverId }, cancellationToken);
            Client? client = await _databaseContext.Clients
                .FindAsync(new object?[] { request.ClientId }, cancellationToken);

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
            return new Response(ride.Id);
        }
    }
}