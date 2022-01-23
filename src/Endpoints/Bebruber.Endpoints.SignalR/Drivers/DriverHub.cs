using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Common.Dto;
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
        var driverId = Guid.Parse(Context.UserIdentifier);

        var request = new UpdateDriverLocation.Command(driverId, coordinate);
        UpdateDriverLocation.Response response = await _mediator.Send(request);

        await Clients.Caller.HandleUpdateLocationResponseAsync(response);
    }
}