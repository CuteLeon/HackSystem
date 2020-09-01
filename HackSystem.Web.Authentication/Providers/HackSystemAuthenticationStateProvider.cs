using System.Runtime.CompilerServices;
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
            var savedToken = await this.cookieStorageService.GetCookieAsync(this.options.AuthTokenName);
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return await this.ReturnAnonymousState();
            }

            var claimsIdentity = this.GetClaimsIdentity(savedToken);
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
        /// 更新认证状态
        /// </summary>
        /// <param name="token"></param>
        public async Task UpdateAuthenticattionStateAsync(string token)
        {
            this.logger.LogInformation("HackSystem Update Authentication State...");
            if (string.IsNullOrWhiteSpace(token))
            {
                await this.AuthenticateFailed();
                return;
            }

            var claimsIdentity = this.GetClaimsIdentity(token);
            if (this.CheckClaimsIdentity(claimsIdentity))
            {
                await this.AuthenticateSuccessfully(claimsIdentity, token);
            }
            else
            {
                await this.AuthenticateFailed();
            }
        }

        /// <summary>
        /// 认证成功
        /// </summary>
        private async Task AuthenticateSuccessfully(ClaimsIdentity claimsIdentity, string token)
        {
            this.logger.LogInformation("HackSystem Authenticate Successfully !");
            await this.cookieStorageService.SaveCookieAsync(this.options.AuthTokenName, token, this.options.TokenExpiryInMinutes * 60);
            var authenticationState = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
            this.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        /// <summary>
        /// 认证失败
        /// </summary>
        private async Task AuthenticateFailed()
        {
            this.logger.LogWarning("HackSystem Authenticate Failed !");
            await this.cookieStorageService.RemoveCookieAsync(this.options.AuthTokenName);
            this.NotifyAuthenticationStateChanged(Task.FromResult(this.options.AnonymousState));
        }
        #endregion

        #region 解析和检查

        /// <summary>
        /// 获取声明组合证件
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private ClaimsIdentity GetClaimsIdentity(string token)
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
        private bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
        {
            this.logger.LogInformation("HackSystem Check Claims Identity...");
            return claimsIdentity.Claims.IsUnexpired(this.options.ExpiryClaimType);
        }
        #endregion
    }
}
