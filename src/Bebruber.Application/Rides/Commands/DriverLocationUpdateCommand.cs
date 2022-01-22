using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using FluentResults;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public class DriverLocationUpdateCommand
{
    public record Command(
        Guid DriverId,
        Coordinate Coord) : IRequest<Response>;
    
    public record Response();

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IDriverLocationService _driverLocationService;
        
        CommandHandler(
            IDriverLocationService driverLocationService,
            )
        {
            _driverLocationService = driverLocationService;
        }
        
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            await _driverLocationService.UpdateDriverLocationAsync()
        }   
    }
}