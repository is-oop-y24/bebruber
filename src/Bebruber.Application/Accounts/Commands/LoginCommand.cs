using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Bebruber.DataAccess;
using Bebruber.Identity;
using Bebruber.Identity.Tools;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bebruber.Application.Accounts.Commands;

public static class LoginCommand
{
    public record CommandA(string Email, string Password) : IRequest<ResponseA>;

    public class CommandValidator : AbstractValidator<CommandA>
    {
        public CommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public record ResponseA(string Token);

    public class CommandHandler : IRequestHandler<CommandA, ResponseA>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private IdentityDatabaseContext _databaseContext;
        private ILogger<CommandHandler> _logger;

        public CommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtTokenGenerator tokenGenerator, ILogger<CommandHandler> logger, IdentityDatabaseContext databaseContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
            _databaseContext = databaseContext;
        }

        public async Task<ResponseA> Handle(CommandA request, CancellationToken cancellationToken)
        {
            _logger.LogWarning(_userManager.FindByEmailAsync("bebra@bebra.bebra").GetAwaiter().GetResult()?.Email ?? "Nope");

            var suser = await _userManager.CreateAsync(new IdentityUser() { Email = "bebra@bebra.bebra", UserName = "Admin" }, "admin");

            _logger.LogWarning(_userManager.FindByEmailAsync("bebra@bebra.bebra").GetAwaiter().GetResult()?.Email ?? "Nope");

            await _databaseContext.SaveChangesAsync(cancellationToken);

            _logger.LogWarning(_userManager.FindByEmailAsync("bebra@bebra.bebra").GetAwaiter().GetResult()?.Email ?? "Nope");

            var user = await _userManager.FindByEmailAsync(request.Email);

           if (user is null)
               throw new AuthenticationException("Wrong email");

           var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

           if (result.Succeeded)
               return new ResponseA(_tokenGenerator.CreateToken(user));

           throw new AuthenticationException("Wrong email or password");
        }
    }
}