using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bebruber.Application.Requests.Rides.Commands;
using Bebruber.Domain.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace Bebruber.Endpoints.Server.Controllers;

[ApiController]
[Route("carCategory")]
public class CarCategoryController : ControllerBase
{
    [HttpGet("categories")]
    public ActionResult<List<string>> GetCarCategories()
    {
        var categories = new List<CarCategory>
        {
            CarCategory.Economy,
            CarCategory.Comfort,
            CarCategory.Business,
        };

        return categories.Select(item => item.Name).ToList();
    }
}