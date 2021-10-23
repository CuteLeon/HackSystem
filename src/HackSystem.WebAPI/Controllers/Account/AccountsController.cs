using System.Security.Claims;
using HackSystem.Common;
using HackSystem.DataTransferObjects.Accounts;
using HackSystem.Intermediary.Application;
using HackSystem.WebAPI.Authentication.Services;
using HackSystem.WebAPI.Domain.Attributes;
using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.Domain.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackSystem.WebAPI.Controllers.Account;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : AuthenticateControllerBase
{
    private readonly ILogger<AccountsController> logger;
    private readonly ITokenGenerator tokenGenerator;
    private readonly IMapper mapper;
    private readonly IIntermediaryPublisher publisher;
    private readonly SignInManager<HackSystemUser> signInManager;

    public AccountsController(
        ILogger<AccountsController> logger,
        ITokenGenerator tokenGenerator,
        IMapper mapper,
        IIntermediaryPublisher publisher,
        SignInManager<HackSystemUser> signInManager,
        RoleManager<HackSystemRole> roleManager,
        UserManager<HackSystemUser> userManager)
        : base(roleManager, userManager)
    {
        this.logger = logger;
        this.tokenGenerator = tokenGenerator;
        this.mapper = mapper;
        this.publisher = publisher;
        this.signInManager = signInManager;
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest register)
    {
        this.logger.LogInformation($"Register new user: {register.UserName}");
        var newUser = new HackSystemUser
        {
            UserName = register.UserName,
            Email = register.Email
        };

        var result = await this.userManager.CreateAsync(newUser, register.Password);
        if (!result.Succeeded)
        {
            this.logger.LogError(new Exception(string.Join("\n", result.Errors)), $"Failed to register: {register.UserName} ({result.Errors.Count()} errors)");
            var errors = result.Errors.Select(x => $"(Code:{x.Code}) {x.Description}");
            var failedResult = new RegisterResponse
            {
                Successful = false,
                Errors = errors
            };
            return this.BadRequest(failedResult);
        }

        result = await this.userManager.AddToRoleAsync(newUser, CommonSense.Roles.HackerRole);
        if (!result.Succeeded)
        {
            this.logger.LogError(new Exception(string.Join("\n", result.Errors)), $"Setup user's roles failed: {register.UserName} ({result.Errors.Count()} errors)");
            var errors = result.Errors.Select(x => $"(Code:{x.Code}) {x.Description}");
            var failedResult = new RegisterResponse
            {
                Successful = false,
                Errors = errors
            };
            return this.BadRequest(failedResult);
        }

        await this.publisher.PublishNotification(new CreateAccountNotification { User = newUser });

        this.logger.LogInformation($"Register successfully: {register.UserName}");
        var registerResult = new RegisterResponse()
        {
            Successful = true
        };
        return this.Ok(registerResult);
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [LogActionFilter(noLogRequestBody: true)]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        this.logger.LogInformation($"Login user: {login.UserName}");
        var result = await this.signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false);
        if (!result.Succeeded)
        {
            var errorMessage = result switch
            {
                { } when result.IsLockedOut => "Account locked out",
                { } when result.IsNotAllowed => "Account not allowed",
                { } when result.RequiresTwoFactor => "Account requires two factor",
                _ => "Invalid user information"
            };

            this.logger.LogWarning($"Login failed: {login.UserName} ({errorMessage})");
            var failedResul = new LoginResponse
            {
                Successful = false,
                Error = errorMessage
            };
            return this.BadRequest(failedResul);
        }

        var claims = await this.GetClaimsAsync(login.UserName);
        var token = this.tokenGenerator.GenerateSecurityToken(claims);
        var loginResul = new LoginResponse
        {
            Successful = true,
            Token = token
        };

        this.logger.LogInformation($"Login successfully: {login.UserName}");
        return this.Ok(loginResul);
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        this.logger.LogInformation($"Logout current user...");
        await this.signInManager.SignOutAsync();
        return this.Ok();
    }

    /// <summary>
    /// Get account information
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAccountInfo()
    {
        var userName = this.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value ??
            throw new ArgumentException(nameof(ClaimTypes.Name));

        var user = await userManager.FindByNameAsync(userName);
        var result = this.mapper.Map<UserResponse>(user);
        return this.Ok(result);
    }
}
