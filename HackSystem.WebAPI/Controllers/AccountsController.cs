using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebDataTransfer.Account;
using HackSystem.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackSystem.WebAPI.Authentication.Services;

namespace HackSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : AuthenticateControllerBase
    {
        private readonly ILogger<AccountsController> logger;
        private readonly ITokenGenerator tokenGenerator;
        private readonly SignInManager<HackSystemUser> signInManager;

        public AccountsController(
            ILogger<AccountsController> logger,
            ITokenGenerator tokenGenerator,
            SignInManager<HackSystemUser> signInManager,
            RoleManager<HackSystemRole> roleManager,
            UserManager<HackSystemUser> userManager)
            : base(roleManager, userManager)
        {
            this.logger = logger;
            this.tokenGenerator = tokenGenerator;
            this.signInManager = signInManager;
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
            var newUser = new HackSystemUser
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

            var claims = await this.GetClaimsAsync(login.UserName);
            var token = this.tokenGenerator.GenerateSecurityToken(claims);
            var loginResul = new LoginResultDTO
            {
                Successful = true,
                Token = token
            };

            this.logger.LogDebug($"登录账户成功: {login.UserName}");
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
