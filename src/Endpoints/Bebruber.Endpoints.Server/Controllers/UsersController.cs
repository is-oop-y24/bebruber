using System.Threading.Tasks;
using Bebruber.Application.Accounts.Commands;
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
    public async Task<ActionResult<LoginCommand.ResponseA>> LoginAsync([FromBody] LoginCommand.CommandA request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        _seeder.Seed();
        await _context.SaveChangesAsync();
        return Ok();
    }
}