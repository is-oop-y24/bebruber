using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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

    [HttpPost("startRide")]
    public async Task<ActionResult<StartRide.Response>> StartRide(StartRide.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("finishRide")]
    public async Task<ActionResult<FinishRide.Response>> FinishRide(FinishRide.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpPatch("arrived")]
    public async Task<ActionResult<DriverArrived.Response>> Arrived(DriverArrived.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("cancel")]
    public async Task<ActionResult<CancelSearch.Response>> Arrived(CancelSearch.Command command)
    {
        return await _mediator.Send(command);
    }
}