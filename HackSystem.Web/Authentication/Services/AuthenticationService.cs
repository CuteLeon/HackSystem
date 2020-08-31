﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Common;
using HackSystem.WebDTO.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HackSystem.Web.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> logger;
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public async Task<RegisterResultDTO> Register(RegisterDTO register)
        {
            logger.LogDebug($"请求注册用户：{register.UserName}");
            var response = await httpClient.PostAsJsonAsync("api/accounts/register", register);
            var registerResult = JsonConvert.DeserializeObject<RegisterResultDTO>(await response.Content.ReadAsStringAsync());
            return registerResult;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<LoginResultDTO> Login(LoginDTO login)
        {
            logger.LogDebug($"请求登录用户：{login.UserName}");
            var response = await httpClient.PostAsJsonAsync("api/accounts/login", login);
            var loginResult = JsonConvert.DeserializeObject<LoginResultDTO>(await response.Content.ReadAsStringAsync());
            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await ((HackSystemAuthenticationStateProvider)this.authenticationStateProvider).UpdateAuthenticattionStateAsync(loginResult.Token);
            return loginResult;
        }

        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccountInfo()
        {
            logger.LogDebug($"请求用户信息...");

            // TODO: Leon: 发送 JWT 头
            var currentToken = await ((HackSystemAuthenticationStateProvider)this.authenticationStateProvider).GetCurrentTokenAsync();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(WebCommonSense.AuthenticationScheme, currentToken);
            var response = await httpClient.GetStringAsync("api/accounts/GetAccountInfo");
            return response;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            logger.LogDebug($"请求注销用户");

            // TODO: Leon: 发送 JWT 头
            await httpClient.GetAsync("api/accounts/logout");
            await ((HackSystemAuthenticationStateProvider)this.authenticationStateProvider).UpdateAuthenticattionStateAsync(string.Empty);
        }
    }
}
