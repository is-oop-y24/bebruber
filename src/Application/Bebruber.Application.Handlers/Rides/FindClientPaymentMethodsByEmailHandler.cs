using Bebruber.DataAccess;
using Bebruber.Domain.Entities;
using Bebruber.Domain.Services;
using Bebruber.Identity;
using Bebruber.Utility.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Command = Bebruber.Application.Requests.Accounts.Queries.FindClientPaymentMethodsByEmail.Command;
using Response = Bebruber.Application.Requests.Accounts.Queries.FindClientPaymentMethodsByEmail.Response;

namespace Bebruber.Application.Handlers.Rides;

public class FindClientPaymentMethodsByEmailHandler : IRequestHandler<Command, Response>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BebruberDatabaseContext _context;

    public FindClientPaymentMethodsByEmailHandler(
        UserManager<ApplicationUser> userManager,
        BebruberDatabaseContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        ApplicationUser? applicationUser = await _userManager.FindByEmailAsync(request.Email);
        Client? user = await _context.FindAsync(
            applicationUser.ModelType,
            applicationUser.ModelId) as Client;
        user = user.ThrowIfNull();
        return new Response(
            user.PaymentInfos.Select(i => i.ToString()).ToList());
    }
}