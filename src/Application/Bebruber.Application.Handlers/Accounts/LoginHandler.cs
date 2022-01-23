using System.Security.Authentication;
using Bebruber.Application.Requests.Accounts;
using Bebruber.DataAccess;
using Bebruber.Identity;
using Bebruber.Identity.Tools;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bebruber.Application.Handlers.Accounts;

public class LoginHandler : IRequestHandler<Login.Command, Login.Response>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly ILogger<LoginHandler> _logger;
    private readonly IdentityDatabaseContext _context;

    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenGenerator tokenGenerator,
        ILogger<LoginHandler> logger,
        IdentityDatabaseContext databaseContext,
        IdentityDatabaseContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
        _context = context;
    }

    public async Task<Login.Response> Handle(Login.Command request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request.Email);
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new AuthenticationException("Wrong email");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
            return new Login.Response(_tokenGenerator.CreateToken(user));

        throw new AuthenticationException("Wrong email or password");
    }
}