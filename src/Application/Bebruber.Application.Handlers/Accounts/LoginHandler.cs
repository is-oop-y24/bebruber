using System.Security.Authentication;
using Bebruber.Application.Requests.Accounts;
using Bebruber.DataAccess;
using Bebruber.Identity.Tools;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bebruber.Application.Handlers.Accounts;

public class LoginCommandHandler : IRequestHandler<Login.Command, Login.Response>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly ILogger<LoginCommandHandler> _logger;
    private readonly IdentityDatabaseContext _context;

    public LoginCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtTokenGenerator tokenGenerator, ILogger<LoginCommandHandler> logger, IdentityDatabaseContext databaseContext, ILogger<LoginCommandHandler> logger1, IdentityDatabaseContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
        _logger = logger1;
        _context = context;
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