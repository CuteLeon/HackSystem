using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.CookieStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.Web.Authentication.Providers
{
    public class HackSystemAuthenticationStateHandler : IHackSystemAuthenticationStateHandler
    {
        private readonly ILogger<HackSystemAuthenticationStateHandler> logger;
        private readonly ICookieStorageService cookieStorageService;
        private readonly HackSystemAuthenticationOptions options;
        private readonly HackSystemAuthenticationStateProvider authenticationStateProvider;

        public HackSystemAuthenticationStateHandler(
            ILogger<HackSystemAuthenticationStateHandler> logger,
            IOptionsMonitor<HackSystemAuthenticationOptions> options,
            AuthenticationStateProvider authenticationStateProvider,
            ICookieStorageService cookieStorageService)
        {
            this.logger = logger;
            this.options = options.CurrentValue;
            this.cookieStorageService = cookieStorageService;
            this.authenticationStateProvider = (HackSystemAuthenticationStateProvider)authenticationStateProvider;
        }

        #region 获取认证信息

        /// <summary>
        /// 获取用户身份认证状态
        /// </summary>
        /// <returns></returns>
        public Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return this.authenticationStateProvider.GetAuthenticationStateAsync();
        }

        /// <summary>
        /// 获取当前 Token
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetCurrentTokenAsync()
        {
            return await this.authenticationStateProvider.GetCurrentTokenAsync();
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

            var claimsIdentity = this.authenticationStateProvider.ParseClaimsIdentity(token);
            if (this.authenticationStateProvider.CheckClaimsIdentity(claimsIdentity))
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

            this.authenticationStateProvider.NotifyAuthenticationStateChanged(authenticationState);
        }

        /// <summary>
        /// 认证失败
        /// </summary>
        private async Task AuthenticateFailed()
        {
            this.logger.LogWarning("HackSystem Authenticate Failed !");
            await this.cookieStorageService.RemoveCookieAsync(this.options.AuthTokenName);

            this.authenticationStateProvider.NotifyAuthenticationStateChanged(this.options.AnonymousState);
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
            return this.authenticationStateProvider.ParseClaimsIdentity(token);
        }

        /// <summary>
        /// 检查声明组合证件
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        public bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
        {
            return this.authenticationStateProvider.CheckClaimsIdentity(claimsIdentity);
        }
        #endregion
    }
}
