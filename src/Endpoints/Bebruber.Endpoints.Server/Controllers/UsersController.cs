using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Accounts;
using Bebruber.DataAccess;
using Bebruber.DataAccess.Seeding;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Bebruber.Endpoints.Server.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private IMediator _mediator;
    private IdentityDatabaseContext _context;

    public UsersController(IMediator mediator, IdentityDatabaseContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Login.Response>> LoginAsync([FromBody] Login.Command request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("register-user")]
    public async Task<ActionResult<RegisterUser.Response>> RegisterUser([FromBody] RegisterUser.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("register-driver")]
    public async Task<RegisterDriver.Response> RegisterDriver([FromBody] RegisterDriver.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet("seed")]
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