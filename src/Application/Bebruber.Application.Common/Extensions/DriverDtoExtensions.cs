using Bebruber.Common.Dto;
using Bebruber.Domain.Entities;

namespace Bebruber.Application.Common.Extensions;

public static class DriverDtoExtensions
{
    public static DriverDto ToDto(this Driver driver)
        => new DriverDto(driver.Name.ToString(), driver.Rating.Value, driver.Car.ToDto());
}