using System.Security.Authentication;
using Bebruber.Application.Requests.Accounts;
using Bebruber.DataAccess;
using Bebruber.Identity.Tools;
using Bebruber.Application.Requests.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bebruber.Application.Handlers.Accounts;

public static class LoginCommand
{
    public class CommandHandler : IRequestHandler<Login.Command, Login.Response>
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

        public async Task<Login.Response> Handle(Login.Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new AuthenticationException("Wrong email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
                return new Login.Response(_tokenGenerator.CreateToken(user));

            throw new AuthenticationException("Wrong email or password");
        }
    }
}