using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public static class RideStartCommand
{
    public record Command(Guid RideId) : IRequest<Response>;

    public record Response(bool Status);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IRideService _rideService;
        private readonly BebruberDatabaseContext _databaseContext;

        public CommandHandler(IRideService rideService, BebruberDatabaseContext databaseContext)
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
}