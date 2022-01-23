using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Accounts;
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
    public async Task<IActionResult> Seed()
    {
        _seeder.Seed();
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("register-user")]
    public async Task<ActionResult<RegisterUser.Response>> RegisterUser(RegisterUser.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("auth")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public IActionResult Get()
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        Console.WriteLine(userIdentity.Name);
        var claims = userIdentity.Claims;
        var roleClaimType = userIdentity.RoleClaimType;
        var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
        roles.ForEach(Console.WriteLine);
        return Ok();
    }

    [HttpPost("check-role")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> CheckRole([FromBody] string role)
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        CheckRole.Response response = await _mediator.Send(new CheckRole.Command(userIdentity, role));
        if (response.IsValid)
            return Ok();
        return Unauthorized();
    }
}