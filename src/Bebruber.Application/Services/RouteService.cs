using System.Collections.Generic;
using System.Threading.Tasks;
using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Application.Services;

public class RouteService : IRouteService
{
    public Task<Route> CreateRouteAsync(Location origin, Location destination, IReadOnlyCollection<Location> intermediatePoints)
    {
        throw new System.NotImplementedException();
    }
}