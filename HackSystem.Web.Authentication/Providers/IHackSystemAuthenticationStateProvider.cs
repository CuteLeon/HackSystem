using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Providers;

/// <summary>
/// Authentication state provider interface
/// </summary>
/// <remarks>
/// Do not inject this interface into DI, and not try to get HackSystemAuthenticationStateProvider service via this interface.
/// Otherwise, auth service can not works well: provider can offer correct, but components having incorrect behaviors.
/// Keep on registing and getting HackSystemAuthenticationStateProvider service via AuthenticationStateProvider type.
/// Stayed up late till 01/09/2020 3:00 AM for this issue.
/// </remarks>
public interface IHackSystemAuthenticationStateProvider
{
    Task<AuthenticationState> GetAuthenticationStateAsync();

    ValueTask<string> GetCurrentTokenAsync();

    void NotifyAuthenticationStateChanged(AuthenticationState authenticationState);

    ClaimsIdentity ParseClaimsIdentity(string token);

    bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity);
}
