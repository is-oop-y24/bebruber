using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Common.Dto;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;
using FluentResults;
using MediatR;

namespace Bebruber.Application.Rides.Commands;

public class DriverLocationUpdateCommand
{
    public record Command(Guid DriverId, CoordinateDto Coord) : IRequest<Response>;
    
    public record Response(bool Status);

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IDriverLocationService _driverLocationService;
        private readonly BebruberDatabaseContext _databaseContext;

        public CommandHandler(
            IDriverLocationService driverLocationService,
            BebruberDatabaseContext databaseContext)
        {
            _driverLocationService = driverLocationService;
            _databaseContext = databaseContext;
        }
        
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            Driver? driver = await _databaseContext.Drivers.FindAsync(request.DriverId);
            driver = driver.ThrowIfNull();
            var coordinate = new Coordinate(request.Coord.Latitude, request.Coord.Longitude);
            await _driverLocationService.UpdateDriverLocationAsync(driver, coordinate, cancellationToken);
            return new Response(true);
        }   
    }
}