using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public class DriverArrivedCommandHandler
{
    public record Command(Guid RideId, Guid ClientId) : IRequest<Response>;
    
    public record Response(bool Status);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IRideService _rideService;
        private readonly BebruberDatabaseContext _databaseContext;
        private readonly IClientNotificationService _clientNotificationService;
        
        public CommandHandler(
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
            Ride? ride = await _databaseContext.Rides.FindAsync(request.RideId);
            Client? client = await _databaseContext.Clients.FindAsync(request.ClientId);
            ride = ride.ThrowIfNull();
            client = client.ThrowIfNull();
            await _rideService.SetRideDriverArrivedAsync(ride, cancellationToken);
            await _clientNotificationService.NotifyDriverArrivedAsync(client, cancellationToken);
            return new Response(true);
        }   
    }
}