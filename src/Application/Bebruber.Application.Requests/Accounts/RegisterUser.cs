using FluentValidation;
using MediatR;

namespace Bebruber.Application.Requests.Accounts;

public class RegisterUser
{
    public record Command(
        string FirstName,
        string MiddleName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Password) : IRequest<Response>;

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.PhoneNumber).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public record Response(Guid Id);
}