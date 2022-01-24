using Bebruber.Domain.Tools;

namespace Bebruber.Application.Handlers.Rides.Exceptions;

public class ClientNotFoundException : BebruberException
{
    public ClientNotFoundException(string email)
        : base($"User with email {email} not found")
    { }
}