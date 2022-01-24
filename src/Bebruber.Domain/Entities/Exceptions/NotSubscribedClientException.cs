using Bebruber.Domain.Tools;

namespace Bebruber.Domain.Entities.Exceptions;

public class NotSubscribedClientException : BebruberException
{
    public NotSubscribedClientException(Client client, Driver driver)
        : base($"{nameof(Client)} {client} not subscribed to {nameof(Driver)} {driver} location updates") { }
}