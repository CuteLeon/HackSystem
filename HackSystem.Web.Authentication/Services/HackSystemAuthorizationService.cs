using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.Web.Authentication.Services
{
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
            logger.LogWarning("<———————————————————— 按需求认证 ————————————————————>");
            logger.LogWarning($"HackSystem 认证服务 => Resource => {resource}");
            logger.LogWarning($"HackSystem 认证服务 => ClaimPrincipal 详细信息=>\n\t{string.Join("\n\t", user.Identities?.Select((i, index) => $"{index}. 名称={i.Name} 标签={i.Label} {(i.IsAuthenticated ? "已" : "未")}认证\n\t\t{string.Join("\n\t\t", i.Claims?.Select((c, index) => $"{index}. {c.Type} => {c.Value}") ?? Enumerable.Empty<string>())}") ?? Enumerable.Empty<string>())}");
            logger.LogWarning($"HackSystem 认证服务 => Requirements 详细信息=>\n\t{string.Join("\n\t", requirements.Select((r, index) => $"{index}. {r}"))}");
            var result = await base.AuthorizeAsync(user, resource, requirements);
            logger.LogWarning($"HackSystem 认证服务 => 结果=>{(result.Succeeded ? "成功" : "失败")} 原因=>\n\t{string.Join("\n\t", result.Failure?.FailedRequirements?.Select((r, index) => $"{index}. {r}") ?? Enumerable.Empty<string>())}");
            return result;
        }

        public async override Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            logger.LogWarning("<———————————————————— 按Policy认证 ————————————————————>");
            logger.LogWarning($"HackSystem 认证服务 => Resource => {resource}");
            logger.LogWarning($"HackSystem 认证服务 => ClaimPrincipal 详细信息=>\n\t{string.Join("\n\t", user.Identities?.Select((i, index) => $"{index}. 名称={i.Name} 标签={i.Label} {(i.IsAuthenticated ? "已" : "未")}认证\n\t\t{string.Join("\n\t\t", i.Claims?.Select((c, index) => $"{index}. {c.Type} => {c.Value}") ?? Enumerable.Empty<string>())}") ?? Enumerable.Empty<string>())}");
            logger.LogWarning($"HackSystem 认证服务 => PolicyName=>{policyName}");
            var result = await base.AuthorizeAsync(user, resource, policyName);
            logger.LogWarning($"HackSystem 认证服务 => 结果=>{(result.Succeeded ? "成功" : "失败")} 原因=>\n\t{string.Join("\n\t", result.Failure?.FailedRequirements?.Select((r, index) => $"{index}. {r}") ?? Enumerable.Empty<string>())}");
            return result;
        }
    }
}
