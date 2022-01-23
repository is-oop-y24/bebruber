using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.StartRide.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.StartRide.Response;

namespace Bebruber.Application.Handlers.Rides;

public class StartRideHandler : IRequestHandler<Command, Response>
{
    private readonly IRideService _rideService;
    private readonly BebruberDatabaseContext _databaseContext;

    public StartRideHandler(IRideService rideService, BebruberDatabaseContext databaseContext)
    {
        _rideService = rideService;
        _databaseContext = databaseContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Ride? ride = await _databaseContext.Rides.FindAsync(new object?[] { request.RideId }, cancellationToken);

        if (ride is not null)
        {
            await _rideService.StartRideAsync(ride, cancellationToken);
        }

        return new Response(ride is not null);
    }
}