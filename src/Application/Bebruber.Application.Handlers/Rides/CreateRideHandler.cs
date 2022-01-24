using Bebruber.Application.Common.Extensions;
using Bebruber.Application.Handlers.Rides.Exceptions;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Command = Bebruber.Application.Requests.Rides.Commands.CreateRide.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.CreateRide.Response;

namespace Bebruber.Application.Handlers.Rides;

public class CreateRideHandler : IRequestHandler<Command, Response>
{
    private readonly IRideQueueService _rideQueueService;
    private readonly BebruberDatabaseContext _databaseContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateRideHandler(IRideQueueService rideQueueService, UserManager<ApplicationUser> userManager, BebruberDatabaseContext databaseContext)
    {
        _rideQueueService = rideQueueService;
        _userManager = userManager;
        _databaseContext = databaseContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var client = await _databaseContext.FindAsync(user.ModelType!, user.ModelId) as Client;

        if (client is null)
            throw new ClientNotFoundException(request.Email);

        var rideEntry = new RideEntry(
            request.Origin.ToLocation(),
            request.Destination.ToLocation(),
            request.IntermediatePoints.Select(p => p.ToLocation()).ToList(),
            client);

        await _rideQueueService.EnqueueRideEntryAsync(rideEntry, cancellationToken);

        return new Response(rideEntry.Id);
    }
}