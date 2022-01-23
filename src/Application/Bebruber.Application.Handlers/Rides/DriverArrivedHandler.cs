using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Utility.Extensions;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.DriverArrived.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.DriverArrived.Response;

namespace Bebruber.Application.Handlers.Rides;

public class DriverArrivedHandler : IRequestHandler<Command, Response>
{
    private readonly IRideService _rideService;
    private readonly BebruberDatabaseContext _databaseContext;
    private readonly IClientNotificationService _clientNotificationService;

    public DriverArrivedHandler(
        IRideService rideService,
        BebruberDatabaseContext databaseContext,
        IClientNotificationService clientNotificationService)
    {
        _rideService = rideService;
        _databaseContext = databaseContext;
        _clientNotificationService = clientNotificationService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        (Guid rideId, Guid clientId) = request;

        Ride? ride = await _databaseContext.Rides.FindAsync(new object?[] { rideId }, cancellationToken);
        Client? client = await _databaseContext.Clients.FindAsync(new object?[] { clientId }, cancellationToken);

        ride = ride.ThrowIfNull();
        client = client.ThrowIfNull();

        await _rideService.SetRideDriverArrivedAsync(ride, cancellationToken);
        await _clientNotificationService.NotifyDriverArrivedAsync(client, cancellationToken);

        return new Response(true);
    }
}