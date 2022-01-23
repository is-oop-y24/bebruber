using Bebruber.Domain.Tools;

namespace Bebruber.Application.Handlers.Rides.Exceptions;

public class WrongRideEntryException : BebruberException
{
    public WrongRideEntryException()
    {
    }

    public WrongRideEntryException(string content)
        : base(content)
    {
    }

    public WrongRideEntryException(string? content, System.Exception? innerException)
        : base(content, innerException)
    {
    }
}