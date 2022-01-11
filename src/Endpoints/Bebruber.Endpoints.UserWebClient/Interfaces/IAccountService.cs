using System.Threading.Tasks;
using Bebruber.Endpoints.UserWebClient.Models;

namespace Bebruber.Endpoints.UserWebClient.Interfaces;

public interface IAccountService
{
    Task Login(LoginModel model);
    Task Register(RegisterModel model);
}