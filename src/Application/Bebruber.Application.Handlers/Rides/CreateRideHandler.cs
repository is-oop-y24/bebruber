using Bebruber.Application.Common.Extensions;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.CreateRide.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.CreateRide.Response;

namespace Bebruber.Application.Handlers.Rides;

public class CreateRideHandler : IRequestHandler<Command, Response>
{
    private readonly IRideQueueService _rideQueueService;

    public CreateRideHandler(IRideQueueService rideQueueService)
    {
        _rideQueueService = rideQueueService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var rideEntry = new RideEntry(
            request.Origin.ToLocation(),
            request.Destination.ToLocation(),
            request.IntermediatePoints.Select(p => p.ToLocation()).ToList());

        await _rideQueueService.EnqueueRideEntryAsync(rideEntry, cancellationToken);

        return new Response(rideEntry.Id);
    }
}