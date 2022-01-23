using Bebruber.Application.Handlers.Accounts.Exceptions;
using Bebruber.Application.Requests.Accounts;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects.User;
using Bebruber.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bebruber.Application.Handlers.Accounts;

public class RegisterUserHandler : IRequestHandler<RegisterUser.Command, RegisterUser.Response>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BebruberDatabaseContext _databaseContext;
    private readonly IdentityDatabaseContext _identityDatabaseContext;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager, BebruberDatabaseContext databaseContext, IdentityDatabaseContext identityDatabaseContext)
    {
        _userManager = userManager;
        _databaseContext = databaseContext;
        _identityDatabaseContext = identityDatabaseContext;
    }

    public async Task<RegisterUser.Response> Handle(RegisterUser.Command request, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
            throw new UserAlreadyExistException(request.Email);

        var client = new Client(
                            new Name(
                                request.FirstName,
                                request.MiddleName,
                                request.LastName),
                            new Rating(10),
                            new PhoneNumber(request.PhoneNumber));

        await _databaseContext.Clients.AddAsync(client, cancellationToken);

        IdentityResult? result = await _userManager.CreateAsync(
            new ApplicationUser()
            {
                ModelType = typeof(ApplicationUser),
                ModelId = client.Id,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = $"{client.Name.FirstName}{client.Name.LastName}",
            },
            request.Password);

        if (!result.Succeeded)

            // TODO: CHANGE!!!!
            throw new Exception($"{string.Join(' ', result.Errors.Select(e => e.Description))}");

        await _identityDatabaseContext.SaveChangesAsync(cancellationToken);

        return new RegisterUser.Response(client.Id);
    }
}