using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Common;
using HackSystem.Web.Configurations;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Services;
using HackSystem.WebDTO.Common;
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
            var apiConfiguration = builder.Configuration.GetSection("APIConfiguration").Get<APIConfiguration>();

            builder.Services
                .Configure<APIConfiguration>(builder.Configuration.GetSection("APIConfiguration"))
                .AddLogging()
                .AddBlazoredLocalStorage()
                .AddCookieStorage()
                .AddAuthorizationCore()
                .AddHackSystemAuthentication(options =>
                {
                    options.AnonymousState.User.Claims.Append(new Claim(ClaimTypes.Name, "Anonymous"));
                    options.AuthenticationURL = apiConfiguration.APIURL;
                    options.TokenExpiryInMinutes = apiConfiguration.TokenExpiryInMinutes;
                    options.AuthenticationScheme = WebCommonSense.AuthenticationScheme;
                    options.AuthenticationType = WebCommonSense.AuthenticationType;
                    options.AuthTokenName = WebCommonSense.AuthTokenName;
                    options.ExpiryClaimType = WebCommonSense.ExpiryClaimType;
                })
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddHttpClient()
                .AddHttpClient<IAuthenticationService, AuthenticationService>(httpClient =>
                {
                    httpClient.BaseAddress = new Uri(apiConfiguration.APIURL);
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
