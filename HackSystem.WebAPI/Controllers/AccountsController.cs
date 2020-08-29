using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HackSystem.WebDTO.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly Logger<AccountsController> logger;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountsController(
            Logger<AccountsController> logger,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            this.logger.LogDebug($"注册新账户: {register.Email}");
            var newUser = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email
            };

            var result = await this.userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                this.logger.LogDebug($"注册账户失败: {register.Email} ({result.Errors.Count()} 个错误)");
                var errors = result.Errors.Select(x => $"(代码:{x.Code}) {x.Description}");
                var failedResult = new RegisterResultDTO
                {
                    Successful = false,
                    Errors = errors
                };
                return this.BadRequest(failedResult);
            }

            this.logger.LogDebug($"注册账户成功: {register.Email}");
            var registerResult = new RegisterResultDTO()
            {
                Successful = true
            };
            return this.Ok(registerResult);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            this.logger.LogDebug($"登录账户: {login.Email}");
            var result = await this.signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (!result.Succeeded)
            {
                var errorMessage = result switch
                {
                    { } when result.IsLockedOut => "账户被锁定",
                    { } when result.IsNotAllowed => "账户被禁用",
                    { } when result.RequiresTwoFactor => "账户需要验证",
                    _ => "邮箱或密码无效"
                };

                this.logger.LogDebug($"登录账户失败: {login.Email} ({errorMessage})");
                var failedResul = new LoginResultDTO
                {
                    Successful = false,
                    Error = errorMessage
                };
                return this.BadRequest(failedResul);
            }

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

            this.logger.LogDebug($"登录账户成功: {login.Email}");
            var loginResul = new LoginResultDTO
            {
                Successful = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return this.Ok(loginResul);
        }
    }
}
