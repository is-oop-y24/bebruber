using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using FluentResults;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public static class AcceptRideCommand
{
    public record Command(
        Guid RideEntryId,
        RideContext RideContext) : IRequest<Response>;
    
    public record Response(
        Guid RideId);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IRideQueueService _rideQueueService;
        private readonly IRideService _rideService;

        CommandHandler(
            IRideQueueService rideQueueService,
            IRideService rideService)
        {
            _rideQueueService = rideQueueService;
            _rideService = rideService;
        }
        
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            Result<RideEntry> result = await _rideQueueService.DequeueRideEntryAsync(
                request.RideEntryId,
                cancellationToken);
            if (result.IsFailed)
            {
                return new Response(Guid.Empty);
            }
            Ride ride = await _rideService.RegisterRideAsync(request.RideContext, cancellationToken);
            return new Response(ride.Id);
        }   
    }
}