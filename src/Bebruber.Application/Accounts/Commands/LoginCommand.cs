using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.DataAccess;
using Bebruber.Identity.Tools;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bebruber.Application.Accounts.Commands;

public static class LoginCommand
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

    public class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public CommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtTokenGenerator tokenGenerator, ILogger<CommandHandler> logger, IdentityDatabaseContext databaseContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

           if (user is null)
               throw new AuthenticationException("Wrong email");

           var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

           if (result.Succeeded)
               return new Response(_tokenGenerator.CreateToken(user));

           throw new AuthenticationException("Wrong email or password");
        }
    }
}