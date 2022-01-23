using System.Collections.Generic;
using System.Threading.Tasks;
using Bebruber.Endpoints.DriverWebClient.Models;
using Bebruber.Endpoints.DriverWebClient.Pages;
using Microsoft.AspNetCore.Components;

namespace Bebruber.Endpoints.DriverWebClient.Services;

public class AccountService
{
    private HttpService _httpService;
    private NavigationManager _navigationManager;
    private LocalStorageService _localStorageService;
    private string _userKey = "user";

    public UserModel User { get; private set; }

    public AccountService(
        HttpService httpService,
        NavigationManager navigationManager,
        LocalStorageService localStorageService)
    {
        _httpService = httpService;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItem<UserModel>(_userKey);
    }

    public async Task Login(LoginModel model)
    {
        User = await _httpService.Post<UserModel>("/users/authenticate", model);
        await _localStorageService.SetItem(_userKey, User);
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItem(_userKey);
        _navigationManager.NavigateTo("account/login");
    }

    public async Task Register(RegisterModel model)
    {
        await _httpService.Post("/users/register", model);
    }

    public async Task<IList<UserModel>> GetAll()
    {
        return await _httpService.Get<IList<UserModel>>("/users");
    }

    public async Task<UserModel> GetById(string id)
    {
        return await _httpService.Get<UserModel>($"/users/{id}");
    }
}