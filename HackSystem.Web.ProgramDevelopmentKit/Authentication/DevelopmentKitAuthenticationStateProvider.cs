using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.ProgramDevelopmentKit.Authentication;

public class DevelopmentKitAuthenticationStateProvider : AuthenticationStateProvider, IDevelopmentKitAuthenticationStateProvider
{
    public AuthenticationState AuthenticationState { get; set; }

    public DevelopmentKitAuthenticationStateProvider()
    {
        var authenticationType = "jwt";
        var expired = (DateTime.Now - new DateTime(1970, 1, 1) + TimeSpan.FromMinutes(30)).TotalSeconds.ToString("#");
        var claims = new[] {
            new Claim(ClaimTypes.Name, "Leon"),
            new Claim(ClaimTypes.Email, "leon@hack.com"),
            new Claim("Professional", "true"),
            new Claim(ClaimTypes.Role, "Commander"),
            new Claim(ClaimTypes.Role, "Hacker"),
            new Claim("exp", expired),
            new Claim("iss", "https://localhost"),
            new Claim("aud", "https://localhost"),
        };
        this.AuthenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType)));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return this.AuthenticationState;
    }
}
