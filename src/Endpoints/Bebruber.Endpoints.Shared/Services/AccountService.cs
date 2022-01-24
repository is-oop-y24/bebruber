using System;
using System.Data;
using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Interfaces;
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
        _navigationManager.NavigateTo("users/login");
    }

    public async Task RegisterDriver<TRegister>(TRegister model)
    {
        await _httpService.PostAsync("/users/register-driver", model);
    }

    public async Task RegisterClient<TRegister>(TRegister model)
    {
        await _httpService.PostAsync("/users/register-user", model);
    }

    public async Task<bool> CheckRole(string role)
    {
        var token = await _localStorageService.GetItemAsync<UserToken>(_userKey);
        try
        {
            await _httpService.PostAsync($"/users/check-role/", role);
            return true;
        }
        catch (DataException)
        {
            return false;
        }
    }
}