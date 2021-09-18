using System.Security.Claims;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.CookieStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Providers;

public class HackSystemAuthenticationStateHandler : IHackSystemAuthenticationStateHandler
{
    private readonly ILogger<HackSystemAuthenticationStateHandler> logger;
    private readonly ICookieStorageService cookieStorageService;
    private readonly IOptionsSnapshot<HackSystemAuthenticationOptions> options;
    private readonly IHackSystemAuthenticationStateProvider authenticationStateProvider;

    public HackSystemAuthenticationStateHandler(
        ILogger<HackSystemAuthenticationStateHandler> logger,
        IOptionsSnapshot<HackSystemAuthenticationOptions> options,
        AuthenticationStateProvider authenticationStateProvider,
        ICookieStorageService cookieStorageService)
    {
        this.logger = logger;
        this.options = options;
        this.cookieStorageService = cookieStorageService;
        this.authenticationStateProvider = authenticationStateProvider as IHackSystemAuthenticationStateProvider;
    }

    #region Get authentication information

    /// <summary>
    /// Get current user's authentication state
    /// </summary>
    /// <returns></returns>
    public Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return this.authenticationStateProvider.GetAuthenticationStateAsync();
    }

    /// <summary>
    /// Get current token
    /// </summary>
    /// <returns></returns>
    public async ValueTask<string> GetCurrentTokenAsync()
    {
        return await this.authenticationStateProvider.GetCurrentTokenAsync();
    }
    #endregion

    #region Update authentication information

    /// <summary>
    /// Update current user's authentication state
    /// </summary>
    /// <param name="token"></param>
    public async Task UpdateAuthenticattionStateAsync(string token)
    {
        this.logger.LogInformation("HackSystem Update Authentication State...");
        if (string.IsNullOrWhiteSpace(token))
        {
            await this.AuthenticateFailed();
            return;
        }

        var claimsIdentity = this.authenticationStateProvider.ParseClaimsIdentity(token);
        if (this.authenticationStateProvider.CheckClaimsIdentity(claimsIdentity))
        {
            await this.AuthenticateSuccessfully(claimsIdentity, token);
        }
        else
        {
            await this.AuthenticateFailed();
        }
    }

    /// <summary>
    /// Authenticate successfully
    /// </summary>
    private async Task AuthenticateSuccessfully(ClaimsIdentity claimsIdentity, string token)
    {
        this.logger.LogInformation("HackSystem Authenticate Successfully !");
        await this.cookieStorageService.SaveCookieAsync(this.options.Value.AuthTokenName, token, this.options.Value.TokenExpiryInMinutes * 60);
        var authenticationState = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));

        this.authenticationStateProvider.NotifyAuthenticationStateChanged(authenticationState);
    }

    /// <summary>
    /// Authenticate failed
    /// </summary>
    private async Task AuthenticateFailed()
    {
        this.logger.LogWarning("HackSystem Authenticate Failed !");
        await this.cookieStorageService.RemoveCookieAsync(this.options.Value.AuthTokenName);

        this.authenticationStateProvider.NotifyAuthenticationStateChanged(this.options.Value.AnonymousState);
    }
    #endregion

    #region Parse and check

    /// <summary>
    /// Parse claims identity
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public ClaimsIdentity ParseClaimsIdentity(string token)
    {
        return this.authenticationStateProvider.ParseClaimsIdentity(token);
    }

    /// <summary>
    /// Check claims identity
    /// </summary>
    /// <param name="claimsIdentity"></param>
    /// <returns></returns>
    public bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
    {
        return this.authenticationStateProvider.CheckClaimsIdentity(claimsIdentity);
    }
    #endregion
}
