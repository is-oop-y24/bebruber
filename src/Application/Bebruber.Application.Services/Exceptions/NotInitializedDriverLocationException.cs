using Bebruber.Domain.Entities;
using Bebruber.Domain.Tools;

namespace Bebruber.Application.Services.Exceptions;

public class NotInitializedDriverLocationException : BebruberException
{
    public NotInitializedDriverLocationException(Driver driver)
        : base($"{nameof(Driver)} {driver} does not have a {nameof(DriverLocation)} initialized") { }
}