using Bebruber.Common.Dto;
using Bebruber.Domain.Entities;

namespace Bebruber.Application.Common.Extensions;

public static class CarDtoExtensions
{
    public static CarDto ToDto(this Car car)
    {
        return new CarDto(
            car.CarNumber.ToString(),
            car.Brand.Value,
            car.Name.Value,
            car.Color.Name,
            car.Color.Value,
            car.Category.Name);
    }
}