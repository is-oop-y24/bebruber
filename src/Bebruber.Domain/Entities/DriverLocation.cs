using System;
using Bebruber.Domain.Tools;
using Bebruber.Domain.ValueObjects.Ride;
using Bebruber.Utility.Extensions;

namespace Bebruber.Domain.Entities;

public class DriverLocation : Entity<DriverLocation>
{
    public DriverLocation(Driver driver, Coordinate coordinate, DateTime lastUpdateTime)
    {
        Driver = driver.ThrowIfNull();
        Coordinate = coordinate.ThrowIfNull();
        LastUpdateTime = lastUpdateTime;
    }

    public Driver Driver { get; private init; }
    public Coordinate Coordinate { get; set; }
    public DateTime LastUpdateTime { get; set; }
}