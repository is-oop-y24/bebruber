using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class AlreadySubscribedClientException : BebruberException
{
    public AlreadySubscribedClientException(Client client, Driver driver)
        : base($"{nameof(Client)} {client} already subscribed to {nameof(Driver)} {driver} location updates") { }
}