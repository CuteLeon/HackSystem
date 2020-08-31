using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.Common;
using HackSystem.Web.Configurations;
using HackSystem.Web.Services.Storage;
using HackSystem.WebDTO.Common;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder
                .InitConfiguration()
                .InitService()
                .InitAuthorizationPolicy();

            await builder.Build().RunAsync();
        }

        public static WebAssemblyHostBuilder InitService(this WebAssemblyHostBuilder builder)
        {
            // 日志服务
            builder.Services.AddLogging();
            // JWT 解析器
            builder.Services.AddScoped<IJWTParser, JWTParser>();
            // 配置本地存储服务
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddCookieStorage();
            // 启用身份认证功能
            builder.Services.AddAuthorizationCore();
            // 注册用户身份认证状态提供者
            builder.Services.AddSingleton<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // 注册指向本应用的 HttpClient 服务
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // 注册 HttpClient 服务
            builder.Services.AddHttpClient();
            // 注册认证服务及其 HttpClient 服务
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetService<APIConfiguration>();
                httpClient.BaseAddress = new Uri(configuration.APIURL);
            });

            return builder;
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebAssemblyHostBuilder InitConfiguration(this WebAssemblyHostBuilder builder)
        {
            var apiConfiguration = builder.Configuration.GetSection("APIConfiguration").Get<APIConfiguration>();
            builder.Services.AddSingleton(apiConfiguration);
            return builder;
        }

        /// <summary>
        /// 配置授权策略
        /// </summary>
        /// <param name="builder"></param>
        /// <remarks>
        /// 需要满足策略的所有需求才可以获得此策略，
        /// 可以在 Authorize 特性的 Policy 验证需要的策略
        /// </remarks>
        /// <returns></returns>
        public static WebAssemblyHostBuilder InitAuthorizationPolicy(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(WebCommonSense.AuthorizationPolicy.HackerPolicy, policy =>
                {
                    policy.RequireRole(CommonSense.Roles.HackerRole);
                });
                options.AddPolicy(WebCommonSense.AuthorizationPolicy.ProfessionalHackerPolicy, policy =>
                {
                    policy.RequireRole(CommonSense.Roles.HackerRole);
                    policy.RequireClaim(CommonSense.Claims.ProfessionalClaim, "true", "TRUE", "True");
                });
                options.AddPolicy(WebCommonSense.AuthorizationPolicy.LeonPolicy, policy =>
                {
                    policy.RequireUserName("Leon");
                });
            });

            return builder;
        }
    }
}
