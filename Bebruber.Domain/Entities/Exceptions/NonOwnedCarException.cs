using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class NonOwnedCarException : BebruberException
{
    public NonOwnedCarException(Driver driver, Car car)
        : base($"{nameof(Driver)} {driver} does not own a {nameof(Car)} {car}") { }
}