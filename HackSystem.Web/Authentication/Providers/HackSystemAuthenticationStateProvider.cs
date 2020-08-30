using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Authentication.Providers
{
    /// <summary>
    /// 用户身份认证状态提供者
    /// </summary>
    public class HackSystemAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string AuthTokenName = "AuthToken";
        private const string AuthenticationType = "jwt";
        private const string AuthenticationScheme = "bearer";

        private readonly ILogger<HackSystemAuthenticationStateProvider> logger;
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        /// <summary>
        /// 身份认证头的值
        /// </summary>
        public AuthenticationHeaderValue AuthenticationHeaderValue { get; protected set; }

        public HackSystemAuthenticationStateProvider(
            ILogger<HackSystemAuthenticationStateProvider> logger,
            HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        /// <summary>
        /// 获取用户身份认证状态
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            logger.LogDebug($"获取用户认证状态...");
            var savedToken = await this.localStorage.GetItemAsync<string>(AuthTokenName);
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                logger.LogDebug($"用户认证状态 = 未登录");
                return new AuthenticationState(new ClaimsPrincipal());
            }

            logger.LogDebug($"用户认证状态 = 未登录");
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, savedToken);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(this.ParseClaimsFromJwt(savedToken), AuthenticationType)));
        }

        /// <summary>
        /// 用户认证成功
        /// </summary>
        /// <param name="token"></param>
        public void MarkUserAsAuthenticated(string token)
        {
            logger.LogDebug($"标记用户认证成功...");
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(this.ParseClaimsFromJwt(token), AuthenticationType));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
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

        /// <summary>
        /// 从 JWT 解析 Claims
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = this.ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        /// <summary>
        /// 解析 Base64 编码
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
