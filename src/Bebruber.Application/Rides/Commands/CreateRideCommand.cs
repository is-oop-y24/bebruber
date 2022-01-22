using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Application.Extensions;
using Bebruber.Common.Dto;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public static class CreateRideCommand
{
    public record Command(
        LocationDto Origin,
        LocationDto Destination,
        IReadOnlyCollection<LocationDto> IntermediatePoints) : IRequest<Response>;


    public record Response(Guid RideEntryId);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IRideQueueService _rideQueueService;

        public CommandHandler(IRideQueueService rideQueueService)
        {
            _rideQueueService = rideQueueService;
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