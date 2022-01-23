using Bebruber.Application.Common.Extensions;
using Bebruber.Common.Dto;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Utility.Extensions;
using MediatR;
using Command = Bebruber.Application.Requests.Rides.Commands.UpdateDriverLocation.Command;
using Response = Bebruber.Application.Requests.Rides.Commands.UpdateDriverLocation.Response;

namespace Bebruber.Application.Handlers.Rides;

public class UpdateDriverLocationHandler : IRequestHandler<Command, Response>
{
    private readonly IDriverLocationService _driverLocationService;
    private readonly BebruberDatabaseContext _databaseContext;

    public UpdateDriverLocationHandler(
        IDriverLocationService driverLocationService, BebruberDatabaseContext databaseContext)
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