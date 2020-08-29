using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HackSystem.Web.Authentication.Services;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // 在本地存储 Tokwn
            builder.Services.AddBlazoredLocalStorage();
            // 启用身份认证功能
            builder.Services.AddAuthorizationCore();
            // 注册用户身份认证状态提供者
            builder.Services.AddScoped<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // 注册指向本应用的 HttpClient 服务
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // 为 Auth 服务注册执行 API 的 HttpClient 服务
            builder.Services.AddScoped(serviceProvider => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // 注册认证服务
            builder.Services.AddHttpClient<IAuthService, AuthService>(httpClient => httpClient.BaseAddress = new Uri("https://localhost:4237"));

            await builder.Build().RunAsync();
        }
    }
}
