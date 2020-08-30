using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.Common;
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
            // �ڱ��ش洢 Tokwn
            builder.Services.AddBlazoredLocalStorage();
            // ���������֤����
            builder.Services.AddAuthorizationCore();
            // ע���û������֤״̬�ṩ��
            builder.Services.AddScoped<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>();
            // ע��ָ��Ӧ�õ� HttpClient ����
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // ע����֤������ HttpClient ����
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                httpClient.BaseAddress = new Uri(configuration["APIURL"]);
            });

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
