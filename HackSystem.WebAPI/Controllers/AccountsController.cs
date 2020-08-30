using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HackSystem.WebAPI.Configurations;
using HackSystem.WebDTO.Account;
using HackSystem.WebDTO.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly JwtConfiguration jwtConfiguration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountsController(
            ILogger<AccountsController> logger,
            SignInManager<IdentityUser> signInManager,
            JwtConfiguration jwtConfiguration,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.jwtConfiguration = jwtConfiguration;
            this.roleManager = roleManager;
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
            this.logger.LogDebug($"注册新账户: {register.UserName}");
            var newUser = new IdentityUser
            {
                UserName = register.UserName,
                Email = register.Email
            };

            var result = await this.userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                this.logger.LogError(new Exception(string.Join("\n", result.Errors)), $"注册账户失败: {register.UserName} ({result.Errors.Count()} 个错误)");
                var errors = result.Errors.Select(x => $"(代码:{x.Code}) {x.Description}");
                var failedResult = new RegisterResultDTO
                {
                    Successful = false,
                    Errors = errors
                };
                return this.BadRequest(failedResult);
            }

            result = await this.userManager.AddToRoleAsync(newUser, CommonSense.Roles.HackerRole);
            if (!result.Succeeded)
            {
                this.logger.LogError(new Exception(string.Join("\n", result.Errors)), $"配置账户角色失败: {register.UserName} ({result.Errors.Count()} 个错误)");
                var errors = result.Errors.Select(x => $"(代码:{x.Code}) {x.Description}");
                var failedResult = new RegisterResultDTO
                {
                    Successful = false,
                    Errors = errors
                };
                return this.BadRequest(failedResult);
            }

            this.logger.LogDebug($"注册账户成功: {register.UserName}");
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
            this.logger.LogDebug($"登录账户: {login.UserName}");
            var result = await this.signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false);
            if (!result.Succeeded)
            {
                var errorMessage = result switch
                {
                    { } when result.IsLockedOut => "账户被锁定",
                    { } when result.IsNotAllowed => "账户被禁用",
                    { } when result.RequiresTwoFactor => "账户需要验证",
                    _ => "邮箱或密码无效"
                };

                this.logger.LogDebug($"登录账户失败: {login.UserName} ({errorMessage})");
                var failedResul = new LoginResultDTO
                {
                    Successful = false,
                    Error = errorMessage
                };
                return this.BadRequest(failedResul);
            }

            var claims = new List<Claim>();
            var user = await this.userManager.FindByNameAsync(login.UserName);
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            var userClaims = await this.userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var roleNames = await this.userManager.GetRolesAsync(user);
            claims.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));
            foreach (var roleName in roleNames)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                var roleClaims = await roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtConfiguration.JwtSecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(this.jwtConfiguration.JwtExpiryInMinutes);

            var token = new JwtSecurityToken(
                this.jwtConfiguration.JwtIssuer,
                this.jwtConfiguration.JwtAudience,
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            this.logger.LogDebug($"登录账户成功: {login.UserName}");
            var loginResul = new LoginResultDTO
            {
                Successful = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return this.Ok(loginResul);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            this.logger.LogDebug($"注销账户...");
            await this.signInManager.SignOutAsync();
            return this.Ok();
        }

        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAccountInfo()
        {
            var userName = this.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value ??
                throw new ArgumentException(nameof(ClaimTypes.Name));

            var user = await userManager.FindByNameAsync(userName);
            return this.Ok(user);
        }
    }
}
