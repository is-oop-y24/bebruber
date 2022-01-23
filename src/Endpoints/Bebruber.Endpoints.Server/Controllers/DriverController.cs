using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

[ApiController]
[Route("api/rides")]
public class DriverController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriverController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("accept")]
    public async Task<ActionResult<AcceptRide.Response>> AcceptRide(AcceptRide.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateRide.Response>> CreateRide(CreateRide.Command command)
    {
        return await _mediator.Send(command);
    }
}