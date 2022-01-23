using Bebruber.Application.Services.Models;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Domain.ValueObjects.Ride;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.Application.Services;

public class DriverLocationService : IDriverLocationService
{
    private readonly BebruberDatabaseContext _context;
    private readonly DriverLocationServiceConfiguration _configuration;
    private readonly ITimeProviderService _timeProviderService;

    public DriverLocationService(
        DriverLocationServiceConfiguration configuration,
        ITimeProviderService timeProviderService,
        BebruberDatabaseContext context)
    {
        _configuration = configuration;
        _timeProviderService = timeProviderService;
        _context = context;
    }

    public async Task<IReadOnlyCollection<Driver>> GetDriversNearbyAsync(
        Coordinate coordinate, CancellationToken cancellationToken)
    {
        DateTime currentDateTime = _timeProviderService.GetCurrentDateTime();

        List<DriverLocation> timeFilteredDriverLocations = await _context.Locations
            .Where(l => currentDateTime - l.LastUpdateTime < _configuration.DeprecationTime)
            .ToListAsync(cancellationToken);

        IEnumerable<DriverLocation> distanceFilteredDriverLocations = timeFilteredDriverLocations
            .Where(l => Math.Abs(l.Coordinate.DistanceBetween(coordinate) - _configuration.NearbyDistance) <=
                        _configuration.DistancePrecision);

        return distanceFilteredDriverLocations.Select(l => l.Driver).ToList();
    }

    public async Task UpdateDriverLocationAsync(
        Driver driver, Coordinate coordinate, CancellationToken cancellationToken)
    {
        DriverLocation? foundDriverLocation = await _context.Locations
            .SingleOrDefaultAsync(l => l.Driver.Equals(driver), cancellationToken);
        DateTime currentDateTime = _timeProviderService.GetCurrentDateTime();

        if (foundDriverLocation is null)
        {
            foundDriverLocation = new DriverLocation(driver, coordinate, currentDateTime);
            await _context.Locations.AddAsync(foundDriverLocation, cancellationToken);
        }
        else
        {
            foundDriverLocation.Coordinate = coordinate;
            foundDriverLocation.LastUpdateTime = currentDateTime;
            _context.Locations.Update(foundDriverLocation);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}