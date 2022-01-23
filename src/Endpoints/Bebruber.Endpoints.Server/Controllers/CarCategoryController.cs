using System.Collections.Generic;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

[ApiController]
[Route("carCategory")]
public class CarCategoryController : ControllerBase
{
    [HttpGet("categories")]
    public ActionResult<List<string>> GetCarCategories()
    {
        var categories = new List<string>
        {
            "Economy",
            "Comfort",
            "Business",
        };

        return categories;
    }
}