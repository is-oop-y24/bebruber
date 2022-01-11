using System.Threading.Tasks;
using Bebruber.Endpoints.DriverWebClient.Models;

namespace Bebruber.Endpoints.DriverWebClient.Interfaces;

public interface IAccountService
{
    Task Login(LoginModel model);
    Task Register(RegisterModel model);
}