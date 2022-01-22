using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Application.Rides.Commands.Exception;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.Tools;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public static class CancelSearchCommand
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
            Result<RideEntry> result = await _rideQueueService.DequeueRideEntryAsync(request.RideId, cancellationToken);
            if (result.IsFailed)
            {
                throw new WrongRideEntryException();
            }
            return new Response(true);
        }
    }
}
