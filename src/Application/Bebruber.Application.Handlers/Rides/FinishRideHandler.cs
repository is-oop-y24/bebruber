using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.FinishRide.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.FinishRide.Response;

namespace Bebruber.Application.Handlers.Rides;

public class FinishRideHandler : IRequestHandler<Command, Response>
{
    private readonly IRideService _rideService;
    private readonly BebruberDatabaseContext _databaseContext;

    public FinishRideHandler(IRideService rideService, BebruberDatabaseContext context)
    {
        _rideService = rideService;
        _databaseContext = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Ride? ride = await _databaseContext.Rides.FindAsync(new object?[] { request.RideId }, cancellationToken);

        if (ride is not null)
        {
            await _rideService.FinishRideAsync(ride, cancellationToken);
        }

        return new Response(ride is not null);
    }
}