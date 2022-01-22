using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Application.Extensions;
using Bebruber.Common.Dto;
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
        LocationDto Origin,
        LocationDto Destination,
        IReadOnlyCollection<LocationDto> IntermediatePoints
            ) : IRequest<Response>
    {
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
                request.Origin.ToLocation(),
                request.Destination.ToLocation(),
                request.IntermediatePoints.Select(p => p.ToLocation()).ToList()
            );

            await _rideQueueService.EnqueueRideEntryAsync(rideEntry, cancellationToken);

            return new Response(rideEntry.Id);
        }
    }
}