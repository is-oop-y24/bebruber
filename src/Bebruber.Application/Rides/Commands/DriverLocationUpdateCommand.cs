using System;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.Application.Extensions;
using Bebruber.Common.Dto;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;
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

        public CommandHandler(IDriverLocationService driverLocationService, BebruberDatabaseContext databaseContext)
        {
            _driverLocationService = driverLocationService;
            _databaseContext = databaseContext;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            (Guid driverId, CoordinateDto? coordinateDto) = request;

            Driver? driver = await _databaseContext.Drivers.FindAsync(new object?[] { driverId }, cancellationToken);
            driver = driver.ThrowIfNull();

            var coordinate = coordinateDto.ToCoordinate();
            await _driverLocationService.UpdateDriverLocationAsync(driver, coordinate, cancellationToken);

            return new Response(true);
        }
    }
}