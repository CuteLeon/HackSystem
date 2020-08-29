using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using HackSystem.WebDTO.Account;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthService(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authenticationStateProvider;
            this.localStorage = localStorage;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public async Task<RegisterResultDTO> Register(RegisterDTO registerModel)
        {
            var response = await httpClient.PostAsJsonAsync("api/accounts/register", registerModel);
            var registerResult = JsonSerializer.Deserialize<RegisterResultDTO>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return registerResult;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public async Task<LoginResultDTO> Login(LoginDTO loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await httpClient.PostAsync("api/accounts/login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResultDTO>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await localStorage.SetItemAsync("authToken", loginResult.Token);
            ((HackSystemAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((HackSystemAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

}
