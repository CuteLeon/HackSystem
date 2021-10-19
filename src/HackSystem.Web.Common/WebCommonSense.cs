namespace HackSystem.Web.Common;

public class WebCommonSense
{

    public const string AuthTokenName = "AuthToken";

    public const string AuthenticationType = "jwt";

    public const string AuthenticationScheme = "bearer";

    public const string ExpiryClaimType = "exp";

    public class AuthorizationPolicy
    {
        public const string HackerPolicy = "HackerPolicy";

        public const string ProfessionalHackerPolicy = "ProfessionalHackerPolicy";

        public const string LeonPolicy = "LeonPolicy";
    }
}
