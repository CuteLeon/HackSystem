using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> logger;

        public TokenController(
            ILogger<TokenController> logger)
        {
            this.logger = logger;
        }

        public async Task<IActionResult> Refresh()
        {
            this.logger.LogDebug($"Refresh Token: {HttpContext.User?.Identity?.Name ?? "[No User Name]"}");
            return default;
        }
    }
}
