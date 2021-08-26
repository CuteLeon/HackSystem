using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.Web.Authentication.Services;

    public class HackSystemAuthorizationService : DefaultAuthorizationService
    {
        private readonly ILogger<DefaultAuthorizationService> logger;

        public HackSystemAuthorizationService(
            IAuthorizationPolicyProvider policyProvider,
            IAuthorizationHandlerProvider handlers,
            ILogger<DefaultAuthorizationService> logger,
            IAuthorizationHandlerContextFactory contextFactory,
            IAuthorizationEvaluator evaluator,
            IOptions<AuthorizationOptions> options)
            : base(policyProvider, handlers, logger, contextFactory, evaluator, options)
        {
            this.logger = logger;
        }

        public async override Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            logger.LogWarning("<-------------- Authorize via requirements -------------->");
            logger.LogWarning($"Authorization Service => Resource => {resource}");
            logger.LogWarning($"Authorization Service => ClaimPrincipal Details=>\n\t{string.Join("\n\t", user.Identities?.Select((i, index) => $"{index}. Name={i.Name} Label={i.Label} {(i.IsAuthenticated ? "Have" : "Not")} authorized\n\t\t{string.Join("\n\t\t", i.Claims?.Select((c, index) => $"{index}. {c.Type} => {c.Value}") ?? Enumerable.Empty<string>())}") ?? Enumerable.Empty<string>())}");
            logger.LogWarning($"Authorization Service => Requirements Details=>\n\t{string.Join("\n\t", requirements.Select((r, index) => $"{index}. {r}"))}");
            var result = await base.AuthorizeAsync(user, resource, requirements);
            logger.LogWarning($"Authorization Service => Result=>{(result.Succeeded ? "Success" : "Fail")} Reason=>\n\t{string.Join("\n\t", result.Failure?.FailedRequirements?.Select((r, index) => $"{index}. {r}") ?? Enumerable.Empty<string>())}");
            return result;
        }

        public async override Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            logger.LogWarning("<-------------- Authorize via policy -------------->");
            logger.LogWarning($"Authorization Service => Resource => {resource}");
            logger.LogWarning($"Authorization Service => ClaimPrincipal Details=>\n\t{string.Join("\n\t", user.Identities?.Select((i, index) => $"{index}. Name={i.Name} Label={i.Label} {(i.IsAuthenticated ? "Have" : "Not")} authorized\n\t\t{string.Join("\n\t\t", i.Claims?.Select((c, index) => $"{index}. {c.Type} => {c.Value}") ?? Enumerable.Empty<string>())}") ?? Enumerable.Empty<string>())}");
            logger.LogWarning($"Authorization Service => PolicyName=>{policyName}");
            var result = await base.AuthorizeAsync(user, resource, policyName);
            logger.LogWarning($"Authorization Service => Result=>{(result.Succeeded ? "success" : "fail")} Reason=>\n\t{string.Join("\n\t", result.Failure?.FailedRequirements?.Select((r, index) => $"{index}. {r}") ?? Enumerable.Empty<string>())}");
            return result;
        }
    }
