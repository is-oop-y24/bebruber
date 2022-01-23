using System.Collections.Generic;
using System.Threading.Tasks;
using Bebruber.Endpoints.DriverWebClient.Interfaces;
using Bebruber.Endpoints.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Bebruber.Endpoints.Shared.Services;

public class AccountService : IAccountService
{
    private IHttpService _httpService;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    private string _userKey = "user";

    public AccountService(
        IHttpService httpService,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService)
    {
        _httpService = httpService;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public UserToken User { get; private set; }

    public async Task Initialize()
    {
        User = await _localStorageService.GetItemAsync<UserToken>(_userKey);
    }

    public async Task Login(LoginModel model)
    {
        User = await _httpService.PostAsync<UserToken>("users/login", model);
        await _localStorageService.SetItemAsync(_userKey, User);
    }

    public async Task Logout()
    {
        User = null;
        await _localStorageService.RemoveItemAsync(_userKey);
        _navigationManager.NavigateTo("account/login");
    }

    public async Task Register<TRegister>(TRegister model)
    {
        await _httpService.PostAsync("/users/register", model);
    }

    public async Task<IList<UserToken>> GetAll()
    {
        return await _httpService.GetAsync<IList<UserToken>>("/users");
    }

    public async Task<UserToken> GetById(string id)
    {
        return await _httpService.GetAsync<UserToken>($"/users/{id}");
    }
}