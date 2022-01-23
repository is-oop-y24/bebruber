using FluentValidation;
using MediatR;

namespace Bebruber.Application.Requests.Accounts;

public class Login
{
    public record Command(string Email, string Password) : IRequest<Response>;

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public record Response(string Token);
}