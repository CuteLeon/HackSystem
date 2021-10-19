using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.AuthorizationStateHandlers;

public interface IHackSystemAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();

    bool ParseValidateClaimsIdentity(string token, out ClaimsIdentity claimsIdentity);

    void NotifyAuthenticationStateChanged(AuthenticationState authenticationState);
}
