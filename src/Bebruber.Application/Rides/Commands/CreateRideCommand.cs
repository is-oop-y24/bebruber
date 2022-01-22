using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Domain.ValueObjects.User;
using FluentValidation;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public static class CreateRideCommand
{
    public record Command(
        Command.Location Origin,
        Command.Location Destination,
        IReadOnlyCollection<Command.Location> IntermediatePoints
            ) : IRequest<Response>
    {
        public record Location(
            Address Address,
            double Latitude,
            double Longitude
        )
        {
            public Domain.ValueObjects.Ride.Location ToDomainLocation() =>
                new Domain.ValueObjects.Ride.Location(
                    new Domain.ValueObjects.Ride.Address(
                        Address.Country,
                        Address.City,
                        Address.Street,
                        Address.Country
                    ),
                    new Coordinate(
                        Latitude,
                        Longitude
                    )
                );
        }

        public record Address(
            string Country,
            string City,
            string Street,
            string House
        );
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
        }
    }

    public record Response(Guid RideEntryId);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private IRideQueueService _rideQueueService;
        private IDriverLocationService _driverLocationService;
        private IDriverNotificationService _driverNotificationService;

        public CommandHandler(IRideQueueService rideQueueService, IDriverLocationService driverLocationService, IDriverNotificationService driverNotificationService)
        {
            _rideQueueService = rideQueueService;
            _driverLocationService = driverLocationService;
            _driverNotificationService = driverNotificationService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var rideEntry = new RideEntry(
                request.Origin.ToDomainLocation(),
                request.Destination.ToDomainLocation(),
                request.IntermediatePoints.Select(p => p.ToDomainLocation()).ToList()
            );

            await _rideQueueService.EnqueueRideEntryAsync(rideEntry, cancellationToken);

            return new Response(rideEntry.Id);
        }
    }
}