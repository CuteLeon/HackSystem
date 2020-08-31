using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.Common;
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
        private readonly HttpClient httpClient;
        private readonly IJWTParser jwtParser;
        private readonly ILocalStorageService localStorage;

        /// <summary>
        /// 身份认证头的值
        /// </summary>
        public AuthenticationHeaderValue AuthenticationHeaderValue { get; protected set; }

        public HackSystemAuthenticationStateProvider(
            ILogger<HackSystemAuthenticationStateProvider> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.serviceScope = serviceScopeFactory.CreateScope();
            this.httpClient = serviceScope.ServiceProvider.GetService<HttpClient>();
            this.jwtParser = serviceScope.ServiceProvider.GetService<IJWTParser>();
            this.localStorage = serviceScope.ServiceProvider.GetService<ILocalStorageService>();
        }

        /// <summary>
        /// 获取用户身份认证状态
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            logger.LogDebug($"获取用户认证状态...");
            var savedToken = await this.localStorage.GetItemAsync<string>(WebCommonSense.AuthTokenName);
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                logger.LogDebug($"用户认证状态 = 未登录");
                return new AuthenticationState(new ClaimsPrincipal());
            }

            logger.LogDebug($"用户认证状态 = 未登录");
            var claims = this.jwtParser.ParseJWTToken(savedToken);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(WebCommonSense.AuthenticationScheme, savedToken);

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, WebCommonSense.AuthenticationType));
            return new AuthenticationState(user);
        }

        /// <summary>
        /// 用户认证成功
        /// </summary>
        /// <param name="token"></param>
        public void MarkUserAsAuthenticated(string token)
        {
            logger.LogDebug($"标记用户认证成功...");
            var claims = this.jwtParser.ParseJWTToken(token);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, WebCommonSense.AuthenticationType));
            var authState = Task.FromResult(new AuthenticationState(user));
            this.NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            logger.LogDebug($"标记用户注销...");
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            this.NotifyAuthenticationStateChanged(authState);
        }
    }
}
