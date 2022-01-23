using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bebruber.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

public class ClientController : ControllerBase
{
    [HttpGet("get-payment-methods")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles="Client")]
    public ActionResult<List<string>> GetPaymentMethods()
    {
        var client = (Client)User.Identity;
        var infos = client.PaymentInfos.Select(item => item.ToString()).ToList();
        return infos;
    }
}