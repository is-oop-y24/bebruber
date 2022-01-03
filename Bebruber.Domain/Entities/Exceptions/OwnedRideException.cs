using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class OwnedRideException<TOwner> : BebruberException
    where TOwner : Entity<TOwner>
{
    public OwnedRideException(TOwner owner, Ride ride)
        : base($"{nameof(Ride)} {ride} is already owned by {typeof(TOwner).Name} {owner}") { }
}