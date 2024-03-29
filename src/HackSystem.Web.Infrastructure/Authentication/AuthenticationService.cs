﻿using System.Net.Http.Json;
using HackSystem.DataTransferObjects.Accounts;
using HackSystem.Web.Application.Authentication;
using HackSystem.Web.Authentication.AuthorizationStateHandlers;
using HackSystem.Web.Authentication.WebServices;
using Newtonsoft.Json;

namespace HackSystem.Web.Infrastructure.Authentication;

public class AuthenticationService : AuthenticatedServiceBase, IAuthenticationService
{
    private readonly IHackSystemAuthenticationStateUpdater authenticationStateUpdater;

    public AuthenticationService(
        ILogger<AuthenticationService> logger,
        IHttpClientFactory httpClientFactory,
        IHackSystemAuthenticationStateUpdater authenticationStateUpdater)
        : base(logger, httpClientFactory)
    {
        this.authenticationStateUpdater = authenticationStateUpdater;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="register"></param>
    /// <returns></returns>
    public async Task<RegisterResponse> Register(RegisterRequest register)
    {
        logger.LogDebug($"Register new user: {register.UserName}");
        var response = await this.HttpClient.PostAsJsonAsync("api/accounts/register", register);
        var registerResult = JsonConvert.DeserializeObject<RegisterResponse>(await response.Content.ReadAsStringAsync());
        return registerResult;
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<LoginResponse> Login(LoginRequest login)
    {
        logger.LogDebug($"Login user: {login.UserName}");
        var response = await this.HttpClient.PostAsJsonAsync("api/accounts/login", login);
        var loginResult = JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await this.authenticationStateUpdater.UpdateAuthenticattionStateAsync(loginResult.Token);
        return loginResult;
    }

    /// <summary>
    /// Get account information
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetAccountInfo()
    {
        logger.LogDebug($"Get account information...");
        var response = await HttpClient.GetAsync("api/accounts/GetAccountInfo");
        if (!response.IsSuccessStatusCode)
        {
            return $"{(int)response.StatusCode} - {response.StatusCode}";
        }

        var accountInfo = await response.Content.ReadAsStringAsync();
        return accountInfo;
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    public async Task Logout()
    {
        logger.LogDebug($"Logout current user...");

        try
        {
            await this.HttpClient.GetAsync("api/accounts/logout");
        }
        catch (Exception ex)
        {
            logger.LogWarning($"Logout Failed: {ex.Message}");
        }

        await this.authenticationStateUpdater.UpdateAuthenticattionStateAsync(string.Empty);
    }
}
