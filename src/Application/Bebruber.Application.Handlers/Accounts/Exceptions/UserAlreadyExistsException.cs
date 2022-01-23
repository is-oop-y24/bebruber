using Bebruber.Domain.Tools;

namespace Bebruber.Application.Handlers.Accounts.Exceptions;

public class UserAlreadyExistException : BebruberException
{
    public UserAlreadyExistException(string email)
        : base($"User with email {email} already exist")
    { }
}