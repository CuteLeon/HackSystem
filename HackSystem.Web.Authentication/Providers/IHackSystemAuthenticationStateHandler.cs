using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Providers;

public interface IHackSystemAuthenticationStateHandler
{
    Task<AuthenticationState> GetAuthenticationStateAsync();

    ValueTask<string> GetCurrentTokenAsync();

    Task UpdateAuthenticattionStateAsync(string token);

    ClaimsIdentity ParseClaimsIdentity(string token);

    bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity);
}
