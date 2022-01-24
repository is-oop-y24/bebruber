using Bebruber.Application.Handlers.Rides.Exceptions;
using Bebruber.Application.Requests.Accounts.Queries;
using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Tools;
using Bebruber.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bebruber.Application.Handlers.Accounts;

public class GetUserIdByEmailHandler : IRequestHandler<GetUserIdByEmail.Query, GetUserIdByEmail.Response>
{
    private UserManager<ApplicationUser> _userManager;
    private BebruberDatabaseContext _databaseContext;

    public GetUserIdByEmailHandler(UserManager<ApplicationUser> userManager, BebruberDatabaseContext databaseContext)
    {
        _userManager = userManager;
        _databaseContext = databaseContext;
    }

    public async Task<GetUserIdByEmail.Response> Handle(GetUserIdByEmail.Query request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var userObject = await _databaseContext.FindAsync(user.ModelType!, user.ModelId);

        var userId = userObject switch
        {
            Client client => client.Id,
            Driver driver => driver.Id,
            _ => throw new ClientNotFoundException(request.Email),
        };

        return new GetUserIdByEmail.Response(userId);
    }
}