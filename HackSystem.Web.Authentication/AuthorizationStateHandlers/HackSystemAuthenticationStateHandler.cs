using System.Security.Claims;
using HackSystem.Web.Authentication.ClaimsIdentityHandlers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.AuthorizationStateHandlers;

/// <summary>
/// User authentication state provider
/// </summary>
public class HackSystemAuthenticationStateHandler : AuthenticationStateProvider, IHackSystemAuthenticationStateHandler
{
    private readonly ILogger<HackSystemAuthenticationStateHandler> logger;
    private readonly IHackSystemAuthenticationTokenHandler hackSystemAuthenticationTokenHandler;
    private readonly IOptionsSnapshot<HackSystemAuthenticationOptions> options;
    private readonly IJsonWebTokenParser jsonWebTokenParser;
    private readonly IHackSystemClaimsIdentityValidator hackSystemClaimsIdentityValidator;

    public HackSystemAuthenticationStateHandler(
        ILogger<HackSystemAuthenticationStateHandler> logger,
        IOptionsSnapshot<HackSystemAuthenticationOptions> options,
        IHackSystemAuthenticationTokenHandler hackSystemAuthenticationTokenHandler,
        IJsonWebTokenParser jsonWebTokenParser,
        IHackSystemClaimsIdentityValidator hackSystemClaimsIdentityValidator)
    {
        this.logger = logger;
        this.hackSystemAuthenticationTokenHandler = hackSystemAuthenticationTokenHandler;
        this.options = options;
        this.jsonWebTokenParser = jsonWebTokenParser;
        this.hackSystemClaimsIdentityValidator = hackSystemClaimsIdentityValidator;
    }

    protected bool ParseValidateClaimsIdentity(string token, out ClaimsIdentity claimsIdentity)
    {
        var claims = this.jsonWebTokenParser.ParseJWTToken(token);
        claimsIdentity = new ClaimsIdentity(claims, this.options.Value.AuthenticationType);
        return this.hackSystemClaimsIdentityValidator.ValidateClaimsIdentity(claimsIdentity);
    }

    /// <summary>
    /// Get current user's authentication state
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        this.logger.LogInformation("HackSystem Get Authentication State...");
        var savedToken = await this.hackSystemAuthenticationTokenHandler.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(savedToken))
        {
            return this.ReturnAnonymousState();
        }

        if (!ParseValidateClaimsIdentity(savedToken, out var claimsIdentity))
        {
            return this.ReturnAnonymousState();
        }

        var user = new ClaimsPrincipal(claimsIdentity);
        return this.ReturnAuthenticatedState(user);
    }

    #region Return authentication state

    /// <summary>
    /// Return anonymous state
    /// </summary>
    /// <returns></returns>
    private AuthenticationState ReturnAnonymousState()
    {
        this.logger.LogInformation("HackSystem Return Anonymous State.");
        return this.options.Value.AnonymousState;
    }

    /// <summary>
    /// Return authenticated state
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    private AuthenticationState ReturnAuthenticatedState(ClaimsPrincipal claimsPrincipal)
    {
        this.logger.LogInformation("HackSystem Return Authenticated State.");
        return new AuthenticationState(claimsPrincipal);
    }

    #endregion


    /// <summary>
    /// Update current user's authentication state
    /// </summary>
    /// <param name="token"></param>
    public async Task UpdateAuthenticattionStateAsync(string token)
    {
        this.logger.LogInformation("HackSystem Update Authentication State...");
        if (string.IsNullOrWhiteSpace(token) ||
            !ParseValidateClaimsIdentity(token, out var claimsIdentity))
        {
            await this.AuthenticateFailed();
            return;
        }

        await this.AuthenticateSuccessfully(token, claimsIdentity);
    }

    #region Update authentication information

    /// <summary>
    /// Authenticate successfully
    /// </summary>
    private async Task AuthenticateSuccessfully(string token, ClaimsIdentity claimsIdentity)
    {
        this.logger.LogWarning("HackSystem Authenticate Successfully !");
        await this.hackSystemAuthenticationTokenHandler.UpdateTokenAsync(token);
        var authenticationState = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
        this.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
    }

    /// <summary>
    /// Authenticate failed
    /// </summary>
    private async Task AuthenticateFailed()
    {
        this.logger.LogWarning("HackSystem Authenticate Failed !");
        await this.hackSystemAuthenticationTokenHandler.RemoveTokenAsync();
        this.NotifyAuthenticationStateChanged(Task.FromResult(this.options.Value.AnonymousState));
    }
    #endregion
}
