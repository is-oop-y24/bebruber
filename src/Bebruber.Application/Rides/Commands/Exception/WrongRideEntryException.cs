using Bebruber.Domain.Tools;

namespace Bebruber.Application.Rides.Commands.Exception;

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