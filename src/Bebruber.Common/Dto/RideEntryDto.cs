namespace Bebruber.Common.Dto;

public record RideEntryDto(
    LocationDto Origin,
    LocationDto Destination,
    IReadOnlyCollection<LocationDto> IntermediatePoints);