using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.AuthorizationStateHandlers;

public interface IHackSystemAuthenticationStateHandler
{
    Task<AuthenticationState> GetAuthenticationStateAsync();

    Task UpdateAuthenticattionStateAsync(string token);
}
