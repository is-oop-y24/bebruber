using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bebruber.Endpoints.Shared.Interfaces;
using Bebruber.Endpoints.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace Bebruber.Endpoints.Shared.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;

    public HttpService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        return await SendRequest<T>(request);
    }

    public async Task PostAsync(string uri, object value)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Post, uri, value);
        await SendRequest(request);
    }

    public async Task<T> PostAsync<T>(string uri, object value)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Post, uri, value);
        return await SendRequest<T>(request);
    }

    public async Task PutAsync(string uri, object value)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Put, uri, value);
        await SendRequest(request);
    }

    public async Task<T> PutAsync<T>(string uri, object value)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Put, uri, value);
        return await SendRequest<T>(request);
    }

    public async Task DeleteAsync(string uri)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Delete, uri);
        await SendRequest(request);
    }

    public async Task<T> DeleteAsync<T>(string uri)
    {
        HttpRequestMessage request = CreateRequest(HttpMethod.Delete, uri);
        return await SendRequest<T>(request);
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object value = null)
    {
        var request = new HttpRequestMessage(method, uri);
        if (value != null)
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return request;
    }

    private async Task SendRequest(HttpRequestMessage request)
    {
        await AddJwtHeader(request);

        // send request
        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/logout");
            return;
        }

        await HandleErrors(response);
    }

    private async Task<T> SendRequest<T>(HttpRequestMessage request)
    {
        await AddJwtHeader(request);

        // send request
        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/logout");
            return default;
        }

        await HandleErrors(response);

        var options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;
        options.Converters.Add(new JsonStringEnumConverter());
        return await response.Content.ReadFromJsonAsync<T>(options);
    }

    private async Task AddJwtHeader(HttpRequestMessage request)
    {
        // add jwt auth header if user is logged in and request is to the api url
        UserToken user = await _localStorageService.GetItemAsync<UserToken>("user");
        bool isApiUrl = !request.RequestUri?.IsAbsoluteUri ?? false;
        if (user is not null && isApiUrl)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
    }

    private async Task HandleErrors(HttpResponseMessage response)
    {
        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            Dictionary<string, string> error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new DataException(error["message"]);
        }
    }
}