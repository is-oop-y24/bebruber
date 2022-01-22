using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
            string Address,
            double Latitude,
            double Longitude
            );
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
        }
    }

    public record Response();

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private IRideQueueService _rideQueueService;

        public CommandHandler(IRideQueueService rideQueueService)
        {
            _rideQueueService = rideQueueService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var rideEntry = new RideEntry(
                new Location(
                        new PaymentAddress(request.Origin.Address)
                    )
                )

            _rideQueueService.EnqueueRideEntryAsync()
        }
    }
}