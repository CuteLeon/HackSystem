using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HackSystem.Web.Authentication.Services;

public class HackSystemAuthorizationHandlerContextFactory : DefaultAuthorizationHandlerContextFactory, IAuthorizationHandlerContextFactory
{
    private readonly ILogger<HackSystemAuthorizationHandlerContextFactory> logger;

    public HackSystemAuthorizationHandlerContextFactory(
        ILogger<HackSystemAuthorizationHandlerContextFactory> logger)
    {
        this.logger = logger;
    }

    public override AuthorizationHandlerContext CreateContext(
        IEnumerable<IAuthorizationRequirement> requirements,
        ClaimsPrincipal user,
        object resource)
    {
        logger.LogWarning("<---------------- Creating authorization context object ---------------->");
        logger.LogWarning($"Creating authorization context object => Resource => {resource}");
        logger.LogWarning($"Creating authorization context object => ClaimPrincipal Details=>\n\t{string.Join("\n\t", user.Identities?.Select((i, index) => $"{index}. Name={i.Name} Label={i.Label} {(i.IsAuthenticated ? "Have" : "Not")} authorized\n\t\t{string.Join("\n\t\t", i.Claims?.Select((c, index) => $"{index}. {c.Type} => {c.Value}") ?? Enumerable.Empty<string>())}") ?? Enumerable.Empty<string>())}");
        logger.LogWarning($"Creating authorization context object => Requirements Details=>\n\t{string.Join("\n\t", requirements.Select((r, index) => $"{index}. {r}"))}");
        var result = base.CreateContext(requirements, user, resource);
        return result;
    }
}
