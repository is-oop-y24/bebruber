using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Common.Dto;
using Bebruber.Utility.Extensions;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Endpoints.SignalR.Drivers;

public class DriverHub : Hub<IDriverClient>
{
    private readonly IMediator _mediator;

    public DriverHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task UpdateLocation(CoordinateDto coordinate)
    {
        // TODO: Proper key obtaining
        var driverId = Guid.Parse(Context.Items["DriverId"].ToString().ThrowIfNull());

        var request = new UpdateDriverLocation.Command(driverId, coordinate);
        UpdateDriverLocation.Response response = await _mediator.Send(request);

        await Clients.Caller.HandleUpdateLocationResponseAsync(response);
    }
}