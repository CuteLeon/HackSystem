using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Authentication.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.InitService();

            await builder.Build().RunAsync();
        }

        public static WebAssemblyHostBuilder InitService(this WebAssemblyHostBuilder builder)
        {
            // 日志服务
#if DEBUG
            builder.Services.AddLogging(options => options.SetMinimumLevel(LogLevel.Debug));
#else
            services.AddLogging(options => options.SetMinimumLevel(LogLevel.Warning));
#endif
            // JWT 解析器
            builder.Services.AddScoped<IJWTParser, JWTParser>();
            // 在本地存储 Tokwn
            builder.Services.AddBlazoredLocalStorage();
            // 启用身份认证功能
            builder.Services.AddAuthorizationCore();
            // 注册用户身份认证状态提供者
            builder.Services.AddScoped<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // 注册指向本应用的 HttpClient 服务
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // 注册认证服务及其 HttpClient 服务
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                httpClient.BaseAddress = new Uri(configuration["APIURL"]);
            });

            return builder;
        }
    }
}
