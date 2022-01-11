using System;
using System.Threading.Tasks;
using Bebruber.Endpoints.DriverWebClient.Interfaces;
using Bebruber.Endpoints.DriverWebClient.Models;

namespace Bebruber.Endpoints.DriverWebClient.Services;

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