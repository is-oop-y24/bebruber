using System.Threading.Tasks;
using Bebruber.Application.Requests.Accounts;
using Bebruber.Common.Dto;
using Bebruber.DataAccess;
using Bebruber.DataAccess.Seeding;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private IMediator _mediator;
    private IdentityDatabaseSeeder _seeder;
    private IdentityDatabaseContext _context;

    public UsersController(IMediator mediator, IdentityDatabaseSeeder seeder, IdentityDatabaseContext context)
    {
        _mediator = mediator;
        _seeder = seeder;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Login.Response>> LoginAsync([FromBody] Login.Command request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("seed")]
    public Task<IActionResult> Seed()
    {
        _seeder.Seed();
        return Task.FromResult<IActionResult>(Ok());
    }
}