using System.Collections.Generic;
using System.Threading.Tasks;
using Bebruber.Domain.Models;
using Bebruber.Domain.ValueObjects.Ride;

namespace Bebruber.Domain.Services;

public interface IRouteService
{
    Task<Route> CreateRouteAsync(Location origin, Location destination, IReadOnlyCollection<Location> intermediatePoints);
}