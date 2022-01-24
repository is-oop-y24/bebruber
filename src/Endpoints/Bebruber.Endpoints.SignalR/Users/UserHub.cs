using Bebruber.Identity;
using Bebruber.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Endpoints.SignalR.Users;

[Authorize(AuthenticationSchemes = "Bearer")]
public class UserHub : Hub<IUserClient>
{
    private UserManager<ApplicationUser> _userManager;

    public UserHub(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override async Task OnConnectedAsync()
    {
        var email = Context.User.Claims.GetEmail();
        Console.WriteLine(email);

        var user = await _userManager.FindByEmailAsync(email);

        Console.WriteLine(user is null);
        Console.WriteLine(user?.ModelId);
    }
}