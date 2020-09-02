using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.CookieStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.Web.Authentication.Providers
{
    /// <summary>
    /// 用户身份认证状态提供者
    /// </summary>
    public class HackSystemAuthenticationStateProvider : AuthenticationStateProvider, IHackSystemAuthenticationStateProvider
    {
        private readonly ILogger<HackSystemAuthenticationStateProvider> logger;
        private readonly IJWTParserService jwtParser;
        private readonly HackSystemAuthenticationOptions options;
        private readonly ICookieStorageService cookieStorageService;

        public HackSystemAuthenticationStateProvider(
            ILogger<HackSystemAuthenticationStateProvider> logger,
            IOptionsMonitor<HackSystemAuthenticationOptions> options,
            IJWTParserService jwtParser,
            ICookieStorageService cookieStorageService)
        {
            this.logger = logger;
            this.options = options.CurrentValue;
            this.jwtParser = jwtParser;
            this.cookieStorageService = cookieStorageService;
        }

        #region 获取认证信息

        /// <summary>
        /// 获取用户身份认证状态
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            this.logger.LogInformation("HackSystem Get Authentication State...");
            var savedToken = await this.GetCurrentTokenAsync();
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return await this.ReturnAnonymousState();
            }

            var claimsIdentity = this.ParseClaimsIdentity(savedToken);
            if (!this.CheckClaimsIdentity(claimsIdentity))
            {
                return await this.ReturnAnonymousState();
            }

            var user = new ClaimsPrincipal(claimsIdentity);
            return await this.ReturnAuthebticatedState(user);
        }

        /// <summary>
        /// 获取当前 Token
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetCurrentTokenAsync()
        {
            this.logger.LogInformation("HackSystem Get Current Token.");
            return await this.cookieStorageService.GetCookieAsync(this.options.AuthTokenName);
        }
        #endregion

        #region 返回认证状态

        /// <summary>
        /// 返回匿名状态
        /// </summary>
        /// <returns></returns>
        private async Task<AuthenticationState> ReturnAnonymousState()
        {
            this.logger.LogInformation("HackSystem Return Anonymous State.");
            return this.options.AnonymousState;
        }

        /// <summary>
        /// 返回已认证状态
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        private async Task<AuthenticationState> ReturnAuthebticatedState(ClaimsPrincipal claimsPrincipal)
        {
            this.logger.LogInformation("HackSystem Return Authebticated State.");
            return new AuthenticationState(claimsPrincipal);
        }

        #endregion

        #region 更新认证信息

        /// <summary>
        /// 通知认证状态改变
        /// </summary>
        /// <param name="authenticationState"></param>
        /// <returns></returns>
        public void NotifyAuthenticationStateChanged(AuthenticationState authenticationState)
        {
            this.logger.LogWarning($"HackSystem Notify Authentication State Change to {authenticationState.User?.Identity?.Name ?? "[null]"}");
            this.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }
        #endregion

        #region 解析和检查

        /// <summary>
        /// 解析声明组合证件
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsIdentity ParseClaimsIdentity(string token)
        {
            this.logger.LogInformation("HackSystem Get Claims Identity from Token...");
            var claims = this.jwtParser.ParseJWTToken(token);
            return new ClaimsIdentity(claims, this.options.AuthenticationType);
        }

        /// <summary>
        /// 检查声明组合证件
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
        {
            this.logger.LogInformation("HackSystem Check Claims Identity...");
            return claimsIdentity.Claims.IsUnexpired(this.options.ExpiryClaimType);
        }
        #endregion
    }
}
