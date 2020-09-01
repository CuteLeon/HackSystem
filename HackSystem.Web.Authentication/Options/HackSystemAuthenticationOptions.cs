using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Options
{
    public class HackSystemAuthenticationOptions
    {
        public string AuthenticationURL { get; set; }

        public string ExpiryClaimType { get; set; } = "exp";

        public string AuthenticationScheme { get; set; } = "bearer";

        public int TokenExpiryInMinutes { get; set; } = 30;

        public readonly AuthenticationState AnonymousState = new AuthenticationState(new ClaimsPrincipal());

        public string AuthTokenName { get; set; } = "AuthToken";

        public string AuthenticationType { get; set; } = "jwt";
    }
}