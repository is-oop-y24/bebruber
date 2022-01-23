using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

[ApiController]
[Route("api/rides")]
public class RidesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RidesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateRide.Response>> CreateRide(CreateRide.Command command)
    {
        return await _mediator.Send(command);
    }
}