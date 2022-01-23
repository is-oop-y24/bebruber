using System.Linq;
using Bebruber.Common.Dto;
using Bebruber.Domain.Entities;

namespace Bebruber.Application.Common.Extensions;

public static class RideEntryDtoExtensions
{
    public static RideEntryDto ToDto(this RideEntry entry)
    {
        return new RideEntryDto(
            entry.Origin.ToDto(),
            entry.Destination.ToDto(),
            entry.IntermediatePoints.Select(LocationDtoExtensions.ToDto).ToList());
    }
}