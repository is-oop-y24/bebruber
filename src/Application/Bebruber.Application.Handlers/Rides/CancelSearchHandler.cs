using Bebruber.Application.Handlers.Rides.Exceptions;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using FluentResults;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.CancelSearch.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.CancelSearch.Response;

namespace Bebruber.Application.Handlers.Rides;

public class CancelSearchHandler : IRequestHandler<Command, Response>
{
    private readonly IRideQueueService _rideQueueService;

    public CancelSearchHandler(IRideQueueService rideQueueService)
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