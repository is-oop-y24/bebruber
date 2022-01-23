namespace Bebruber.Common.Dto;

public record LocationDto(
    AddressDto Address,
    CoordinateDto CoordinateDto);