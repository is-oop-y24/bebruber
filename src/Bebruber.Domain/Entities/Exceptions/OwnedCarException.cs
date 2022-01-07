using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class OwnedCarException : BebruberException
{
    public OwnedCarException(Driver driver, Car car)
        : base($"{nameof(Driver)} {driver} already owns a {nameof(Car)} {car}") { }
}