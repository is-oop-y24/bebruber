using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

public class ClientController : ControllerBase
{
    [HttpGet("get-payment-methods")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles="Client")]
    public ActionResult<List<string>> GetPaymentMethods()
    {
        var userIdentity = (ClaimsIdentity)User.Identity;
        foreach (var item in userIdentity.Claims)
        {
        }

        return new List<string>();
    }
}