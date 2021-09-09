using System.Security.Claims;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.ProgramDevelopmentKit.Authentication;

public class DevelopmentKitAuthenticationStateProvider : AuthenticationStateProvider, IHackSystemAuthenticationStateProvider
{
    public AuthenticationState AuthenticationState { get; set; }

    public string Token { get; set; }

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

        this.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOiJ0cnVlIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjI1MzQwMjI3MjAwMCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3QiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdCJ9.umAjNPQpwDprNQZFSPSTRFdP0CVtC9KqLU4n5c5a2Lk";
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return this.AuthenticationState;
    }

    public async ValueTask<string> GetCurrentTokenAsync()
    {
        return this.Token;
    }

    public void NotifyAuthenticationStateChanged(AuthenticationState authenticationState)
    {
        throw new NotSupportedException();
    }

    public ClaimsIdentity ParseClaimsIdentity(string token)
    {
        throw new NotSupportedException();
    }

    public bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
    {
        throw new NotSupportedException();
    }
}
