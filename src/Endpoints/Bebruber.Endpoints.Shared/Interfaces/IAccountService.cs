using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Models;

namespace Bebruber.Endpoints.Shared.Interfaces;

public interface IAccountService
{
    UserToken User { get; }
    Task Initialize();
    Task Login(LoginModel model);
    Task Logout();
    Task RegisterDriver<TRegister>(TRegister model);
    Task RegisterClient<TRegister>(TRegister model);
    Task<bool> CheckRole(string role);
}
