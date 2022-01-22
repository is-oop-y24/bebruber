using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class AlreadyDismissedRideEntryException : BebruberException
{
    public AlreadyDismissedRideEntryException(RideEntry entry, Driver driver)
        : base($"{nameof(Driver)} {driver} already dismissed {nameof(RideEntry)} {entry}") { }
}