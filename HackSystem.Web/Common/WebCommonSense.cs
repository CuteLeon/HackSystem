using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Common
{
    public class WebCommonSense
    {
        /// <summary>
        /// 匿名者
        /// </summary>
        public static readonly ClaimsPrincipal Anonymous = new ClaimsPrincipal() { };

        /// <summary>
        /// 匿名状态
        /// </summary>
        public static readonly AuthenticationState AnonymousState = new AuthenticationState(Anonymous);

        public const string AuthTokenName = "AuthToken";

        public const string AuthenticationType = "jwt";

        public const string AuthenticationScheme = "bearer";

        public class AuthorizationPolicy
        {
            public const string HackerPolicy = "HackerPolicy";

            public const string ProfessionalHackerPolicy = "ProfessionalHackerPolicy";

            public const string LeonPolicy = "LeonPolicy";
        }
    }
}
