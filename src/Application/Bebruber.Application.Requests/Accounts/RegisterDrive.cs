using FluentValidation;
using MediatR;

namespace Bebruber.Application.Requests.Accounts;

public class RegisterDriver
{
    public record Command(string FirstName,
        string MiddleName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Password,
        string CarName,
        string CarNumber,
        string CarBrand,
        string CarColor,
        string CarCategory) : IRequest<Response>;

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