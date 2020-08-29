using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HackSystem.WebDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public LoginController(
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await this.signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded) return this.BadRequest(new LoginResultDTO { Successful = false, Error = "Username and password are invalid." });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(Convert.ToInt32(this.configuration["JwtExpiryInMinutes"]));

            var token = new JwtSecurityToken(
                this.configuration["JwtIssuer"],
                this.configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return this.Ok(new LoginResultDTO { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
