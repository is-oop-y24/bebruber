using System;
using System.Threading.Tasks;
using Bebruber.Endpoints.UserWebClient.Interfaces;
using Bebruber.Endpoints.UserWebClient.Models;

namespace Bebruber.Endpoints.UserWebClient.Services;

public class AccountService : IAccountService
{
    public async Task Login(LoginModel model)
    {
        throw new NotImplementedException();
    }

    public Task Register(RegisterModel model)
    {
        throw new NotImplementedException();
    }
}