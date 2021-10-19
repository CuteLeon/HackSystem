using System.Security.Claims;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Options;

namespace HackSystem.Web.Authentication.ClaimsIdentityHandlers;

public class HackSystemClaimsIdentityValidator : IHackSystemClaimsIdentityValidator
{
    private readonly ILogger<HackSystemClaimsIdentityValidator> logger;
    private readonly IOptionsSnapshot<HackSystemAuthenticationOptions> options;

    public HackSystemClaimsIdentityValidator(
        ILogger<HackSystemClaimsIdentityValidator> logger,
        IOptionsSnapshot<HackSystemAuthenticationOptions> options)
    {
        this.logger = logger;
        this.options = options;
    }

    public bool ValidateClaimsIdentity(ClaimsIdentity claimsIdentity)
    {
        this.logger.LogDebug("HackSystem validate Claims Identity...");
        return !claimsIdentity.Claims.IsExpired(this.options.Value.ExpiryClaimType);
    }
}
