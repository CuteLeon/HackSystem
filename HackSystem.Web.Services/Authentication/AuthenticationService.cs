using System.Net.Http.Json;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.Extensions;
using HackSystem.WebDataTransfer.Account;
using Newtonsoft.Json;

namespace HackSystem.Web.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> logger;
    private readonly HttpClient httpClient;
    private readonly IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler;

    public AuthenticationService(
        ILogger<AuthenticationService> logger,
        HttpClient httpClient,
        IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler)
    {
        this.logger = logger;
        this.httpClient = httpClient;
        this.hackSystemAuthenticationStateHandler = hackSystemAuthenticationStateHandler;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="register"></param>
    /// <returns></returns>
    public async Task<RegisterResultDTO> Register(RegisterDTO register)
    {
        logger.LogDebug($"Register new user: {register.UserName}");
        var response = await httpClient.PostAsJsonAsync("api/accounts/register", register);
        var registerResult = JsonConvert.DeserializeObject<RegisterResultDTO>(await response.Content.ReadAsStringAsync());
        return registerResult;
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<LoginResultDTO> Login(LoginDTO login)
    {
        logger.LogDebug($"Login user: {login.UserName}");
        var response = await httpClient.PostAsJsonAsync("api/accounts/login", login);
        var loginResult = JsonConvert.DeserializeObject<LoginResultDTO>(await response.Content.ReadAsStringAsync());
        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await this.hackSystemAuthenticationStateHandler.UpdateAuthenticattionStateAsync(loginResult.Token);
        return loginResult;
    }

    /// <summary>
    /// Get account information
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetAccountInfo()
    {
        logger.LogDebug($"Get account information...");

        var currentToken = await this.hackSystemAuthenticationStateHandler.GetCurrentTokenAsync();
        httpClient.AddAuthorizationHeader(currentToken);
        var response = await httpClient.GetAsync("api/accounts/GetAccountInfo");
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
            var currentToken = await this.hackSystemAuthenticationStateHandler.GetCurrentTokenAsync();
            httpClient.AddAuthorizationHeader(currentToken);
            await httpClient.GetAsync("api/accounts/logout");
        }
        catch (Exception ex)
        {
            logger.LogWarning($"Logout Failed: {ex.Message}");
        }

        await this.hackSystemAuthenticationStateHandler.UpdateAuthenticattionStateAsync(string.Empty);
    }
}
