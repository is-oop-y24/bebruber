using System.Linq;
using Bebruber.Common.Dto;
using Bebruber.Domain.Entities;

namespace Bebruber.Application.Common.Extensions;

public static class RideDtoExtensions
{
    public static RideDto ToDto(this Ride ride)
    {
        return new RideDto(
            ride.Client.ToDto(),
            ride.Driver.ToDto(),
            ride.Cost.Value,
            ride.Origin.ToDto(),
            ride.Destination.ToDto(),
            ride.IntermediatePoints.Select(LocationDtoExtensions.ToDto).ToList());
    }
}