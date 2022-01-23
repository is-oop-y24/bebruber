using Bebruber.Domain.Tools;

namespace Bebruber.Application.Accounts.Commands.Exceptions;

public class UserNotFoundException : BebruberException
{
    public UserNotFoundException(string email)
        : base($"User with email {email} not found")
    { }
}