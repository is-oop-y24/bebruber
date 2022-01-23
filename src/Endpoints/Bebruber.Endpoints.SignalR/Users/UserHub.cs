using Microsoft.AspNetCore.SignalR;

namespace Bebruber.Endpoints.SignalR.Users;

public class UserHub : Hub<IUserClient> { }