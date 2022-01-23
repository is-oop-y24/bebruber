using System.Collections.Generic;
using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Models;

namespace Bebruber.Endpoints.DriverWebClient.Interfaces;

public interface IAccountService
{
    UserToken User { get; }
    Task Initialize();
    Task Login(LoginModel model);
    Task Logout();
    Task Register<TRegister>(TRegister model);
}
