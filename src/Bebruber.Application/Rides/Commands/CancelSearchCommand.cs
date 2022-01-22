using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Services;
using FluentValidation;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public class CancelSearchCommand
{
    public record Command(Guid RideId) : IRequest<Response>;

    public record Response(bool Status);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IRideQueueService _rideQueueService;

        public CommandHandler(IRideQueueService rideQueueService)
        {
            _rideQueueService = rideQueueService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            await _rideQueueService.DequeueRideEntryAsync(request.RideId, cancellationToken);
            return new Response(true);
        }
    }
}
