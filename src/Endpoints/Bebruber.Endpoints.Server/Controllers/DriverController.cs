using System.Security.Claims;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Utility.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<CreateRide.Response>> CreateRide([FromBody]CreateRide.ExternalCommand command)
    {
        var email = ((ClaimsIdentity)User.Identity)?.Claims.GetEmail();

        var newCommand = new CreateRide.Command(
            email,
            command.Origin,
            command.Destination,
            command.TaxiCategory,
            command.PaymentMethod,
            command.IntermediatePoints);
        return await _mediator.Send(newCommand);
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