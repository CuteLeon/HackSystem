using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountsController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterDTO model)
        {
            var newUser = new IdentityUser { UserName = model.Email, Email = model.Email };

            var result = await this.userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return this.BadRequest(new RegisterResultDTO { Successful = false, Errors = errors });
            }

            return this.Ok(new RegisterResultDTO { Successful = true });
        }
    }
}
