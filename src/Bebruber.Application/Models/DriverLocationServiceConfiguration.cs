using System;

namespace Bebruber.Application.Models;

public class DriverLocationServiceConfiguration
{
    public DriverLocationServiceConfiguration(
        double nearbyDistance, TimeSpan deprecationTime, double distancePrecision = 0.0001)
    {
        NearbyDistance = nearbyDistance;
        DeprecationTime = deprecationTime;
        DistancePrecision = distancePrecision;
    }

    public double NearbyDistance { get; }
    public double DistancePrecision { get; }
    public TimeSpan DeprecationTime { get; }
}