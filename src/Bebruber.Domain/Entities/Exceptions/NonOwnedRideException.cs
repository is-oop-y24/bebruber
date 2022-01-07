using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class NonOwnedRideException<TOwner> : BebruberException
    where TOwner : Entity<TOwner>
{
    public NonOwnedRideException(TOwner owner, Ride ride)
        : base($"{nameof(Ride)} {ride} is not owned by {typeof(TOwner).Name} {owner}") { }
}