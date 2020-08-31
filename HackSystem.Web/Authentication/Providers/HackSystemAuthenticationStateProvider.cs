using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.Common;
using HackSystem.Web.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Authentication.Providers
{
    /// <summary>
    /// 用户身份认证状态提供者
    /// </summary>
    public class HackSystemAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IServiceScope serviceScope;
        private readonly ILogger<HackSystemAuthenticationStateProvider> logger;
        private readonly IJWTParser jwtParser;
        private readonly ILocalStorageService localStorage;

        public HackSystemAuthenticationStateProvider(
            ILogger<HackSystemAuthenticationStateProvider> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScope = serviceScopeFactory.CreateScope();
            this.jwtParser = serviceScope.ServiceProvider.GetService<IJWTParser>();
            this.localStorage = serviceScope.ServiceProvider.GetService<ILocalStorageService>();
        }

        #region 获取认证信息

        /// <summary>
        /// 获取用户身份认证状态
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            this.logger.LogInformation("Get Authentication State...");
            var savedToken = await this.localStorage.GetItemAsync<string>(WebCommonSense.AuthTokenName);
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return WebCommonSense.AnonymousState;
            }

            var claimsIdentity = this.GetClaimsIdentity(savedToken);
            if (!this.CheckClaimsIdentity(claimsIdentity))
            {
                return WebCommonSense.AnonymousState;
            }

            var user = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationState(user);
        }

        /// <summary>
        /// 获取当前 Token
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetCurrentTokenAsync()
        {
            return await this.localStorage.GetItemAsStringAsync(WebCommonSense.AuthTokenName);
        }
        #endregion

        #region 更新认证信息

        /// <summary>
        /// 更新认证状态
        /// </summary>
        /// <param name="token"></param>
        public async Task UpdateAuthenticattionStateAsync(string token)
        {
            this.logger.LogInformation("Update Authentication State...");
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
            this.logger.LogInformation("Authenticate Successfully !");
            await this.localStorage.SetItemAsync(WebCommonSense.AuthTokenName, token);
            var authenticationState = new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
            this.NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        /// <summary>
        /// 认证失败
        /// </summary>
        private async Task AuthenticateFailed()
        {
            this.logger.LogWarning("Authenticate Failed !");
            await this.localStorage.RemoveItemAsync(WebCommonSense.AuthTokenName);
            this.NotifyAuthenticationStateChanged(Task.FromResult(WebCommonSense.AnonymousState));
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
            this.logger.LogInformation("Get Claims Identity from Token...");
            var claims = this.jwtParser.ParseJWTToken(token);
            return new ClaimsIdentity(claims, WebCommonSense.AuthenticationType);
        }

        /// <summary>
        /// 检查声明组合证件
        /// </summary>
        /// <param name="claimsIdentity"></param>
        /// <returns></returns>
        private bool CheckClaimsIdentity(ClaimsIdentity claimsIdentity)
        {
            this.logger.LogInformation("Check Claims Identity...");
            return claimsIdentity.Claims.IsUnexpired();
        }
        #endregion
    }
}
