using Bebruber.Application.Requests.Accounts.Queries;
using Bebruber.Identity;
using Bebruber.Utility.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Endpoints.SignalR;

public class ClientIdProvider : IUserIdProvider
{
    private IMediator _mediator;

    public ClientIdProvider(IMediator mediator)
    {
        _mediator = mediator;
    }

    public string GetUserId(HubConnectionContext connection)
    {
        var email = connection.User.Claims.GetEmail();
        return email;
    }
}