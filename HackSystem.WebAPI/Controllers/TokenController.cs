using System.Threading.Tasks;
using HackSystem.WebAPI.Authentication.Services;
using HackSystem.WebAPI.Model.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TokenController : AuthenticateControllerBase
    {
        private readonly ILogger<TokenController> logger;
        private readonly ITokenGenerator tokenGenerator;

        public TokenController(
            ILogger<TokenController> logger,
            ITokenGenerator tokenGenerator,
            RoleManager<HackSystemRole> roleManager,
            UserManager<HackSystemUser> userManager)
            : base(roleManager, userManager)
        {
            this.logger = logger;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<IActionResult> Refresh()
        {
            var userName = this.HttpContext.User?.Identity?.Name;
            this.logger.LogDebug($"Refresh Token: {userName ?? "[No User Name]"}");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return this.BadRequest();
            }

            var claims = await this.GetClaimsAsync(userName);
            var token = this.tokenGenerator.GenerateSecurityToken(claims);
            return this.Ok(token);
        }
    }
}
