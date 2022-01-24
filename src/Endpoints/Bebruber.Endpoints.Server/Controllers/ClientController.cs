using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Accounts;
using Bebruber.Application.Requests.Accounts.Queries;
using Bebruber.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-payment-methods")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<FindClientPaymentMethodsByEmail.Response>> GetPaymentMethods()
    {
        var userIdentity = (ClaimsIdentity)User.Identity;

        Claim claim = userIdentity.Claims
            .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
        string email = claim.Value;

        var command = new FindClientPaymentMethodsByEmail.Command(email);
        return await _mediator.Send(command);
    }
}