using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public class RideCommand
{
    public record Command(
        RideEntry RideEntry) : IRequest<Response>;
    
    public record Response();

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IRideQueueService _rideQueueService;
        
        CommandHandler(IRideQueueService rideQueueService)
        {
            _rideQueueService = rideQueueService;
        }
        
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            await _rideQueueService.EnqueueRideEntryAsync(request.RideEntry, cancellationToken);
            return new Response();
        }   
    }
}