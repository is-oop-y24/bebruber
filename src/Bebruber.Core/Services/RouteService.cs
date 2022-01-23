using Bebruber.Domain.Models;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Core.Services;

public class RouteService : IRouteService
{
    public Task<Route> CreateRouteAsync(Location origin, Location destination, IReadOnlyCollection<Location> intermediatePoints)
    {
        var coordinates = intermediatePoints.Select(item => item.Coordinate).ToList();
        var sectors = new List<RouteSector>();
        for (int i = 0; i < coordinates.Count - 1; ++i)
        {
            sectors.Add(new RouteSector(coordinates[i], coordinates[i + 1], 0));
        }
        return Task.FromResult(new Route(sectors));
    }
}