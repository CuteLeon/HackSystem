using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HackSystem.Web.Authentication.Services;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using Microsoft.AspNetCore.Components.Authorization;
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

        public static WebAssemblyHostBuilder InitConfig(this WebAssemblyHostBuilder builder)
        {
            // TODO: 使用配置服务向自身应用请求json文件内的配置
            const string ConfigFileName = "appsettings.json";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigFileName)
                .Build();
            builder.Services.AddSingleton(configuration);
            return builder;
        }

        public static WebAssemblyHostBuilder InitService(this WebAssemblyHostBuilder builder)
        {
            // 日志服务
#if DEBUG
            builder.Services.AddLogging(options => options.SetMinimumLevel(LogLevel.Debug));
#else
            services.AddLogging(options => options.SetMinimumLevel(LogLevel.Warning));
#endif

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
                httpClient.BaseAddress = new Uri("https://localhost:4237");
            });

            return builder;
        }
    }
}
