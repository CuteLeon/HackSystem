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
            // ��־����
            builder.Services.AddLogging();
            // JWT ������
            builder.Services.AddScoped<IJWTParser, JWTParser>();
            // ���ñ��ش洢����
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddCookieStorage();
            // ���������֤����
            builder.Services.AddAuthorizationCore();
            // ע���û������֤״̬�ṩ��
            builder.Services.AddSingleton<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // ע��ָ��Ӧ�õ� HttpClient ����
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // ע�� HttpClient ����
            builder.Services.AddHttpClient();
            // ע����֤������ HttpClient ����
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetService<APIConfiguration>();
                httpClient.BaseAddress = new Uri(configuration.APIURL);
            });

            return builder;
        }

        /// <summary>
        /// ��ʼ������
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
        /// ������Ȩ����
        /// </summary>
        /// <param name="builder"></param>
        /// <remarks>
        /// ��Ҫ������Ե���������ſ��Ի�ô˲��ԣ�
        /// ������ Authorize ���Ե� Policy ��֤��Ҫ�Ĳ���
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
