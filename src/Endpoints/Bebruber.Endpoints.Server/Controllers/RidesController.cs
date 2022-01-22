using System.Threading.Tasks;
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
    public async Task<IActionResult> CreateRide()
    {
        _mediator.Send();
    }
}