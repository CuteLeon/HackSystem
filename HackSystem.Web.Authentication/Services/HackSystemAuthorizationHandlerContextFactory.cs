using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Authentication.Services
{
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
            logger.LogWarning("<———————————————————— 创建认账处理程序交互对象 ————————————————————>");
            logger.LogWarning($"HackSystem 创建交互对象 => Resource => {resource}");
            logger.LogWarning($"HackSystem 创建交互对象 => ClaimPrincipal 详细信息=>\n\t{string.Join("\n\t", user.Identities?.Select((i, index) => $"{index}. 名称={i.Name} 标签={i.Label} {(i.IsAuthenticated ? "已" : "未")}认证\n\t\t{string.Join("\n\t\t", i.Claims?.Select((c, index) => $"{index}. {c.Type} => {c.Value}") ?? Enumerable.Empty<string>())}") ?? Enumerable.Empty<string>())}");
            logger.LogWarning($"HackSystem 创建交互对象 => Requirements 详细信息=>\n\t{string.Join("\n\t", requirements.Select((r, index) => $"{index}. {r}"))}");
            var result = base.CreateContext(requirements, user, resource);
            return result;
        }
    }
}
