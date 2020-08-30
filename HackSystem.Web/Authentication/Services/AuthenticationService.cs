using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
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
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.localStorage = localStorage;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public async Task<RegisterResultDTO> Register(RegisterDTO register)
        {
            logger.LogDebug($"请求注册用户：{register.Email}");
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
            logger.LogDebug($"请求登录用户：{login.Email}");
            var response = await httpClient.PostAsJsonAsync("api/accounts/login", login);
            var loginResult = JsonConvert.DeserializeObject<LoginResultDTO>(await response.Content.ReadAsStringAsync());
            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await localStorage.SetItemAsync(WebCommonSense.AuthTokenName, loginResult.Token);
            ((HackSystemAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(WebCommonSense.AuthenticationScheme, loginResult.Token);

            return loginResult;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            logger.LogDebug($"请求注销用户");
            _ = await httpClient.GetAsync("api/accounts/logout");
            await localStorage.RemoveItemAsync(WebCommonSense.AuthTokenName);
            ((HackSystemAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
