using System.Drawing;

namespace Bebruber.Common.Dto;

public record CarDto(
    string Number,
    string Brand,
    string Name,
    string ColorName,
    Color Color,
    string Category
);