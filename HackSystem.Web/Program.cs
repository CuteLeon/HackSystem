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
using HackSystem.Web.Scheduler.Program;
using HackSystem.Web.Services.Authentication;
using HackSystem.Web.Services.API.Program;
using HackSystem.Web.Services.Program;
using HackSystem.Common;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Extensions;
using HackSystem.Observer;

namespace HackSystem.Web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var apiConfiguration = builder.Configuration.GetSection("APIConfiguration").Get<APIConfiguration>();

            builder.RootComponents.Add<App>("#app");

            builder
                .InitializeConfiguration()
                .InitializeBasicService(apiConfiguration)
                .InitializeHackSystemServices(apiConfiguration)
                .InitializeAuthorizationPolicy();

            await builder
                .Build()
                .LaunchBasicServices()
                .RunAsync();
        }

        public static WebAssemblyHostBuilder InitializeBasicService(this WebAssemblyHostBuilder builder, APIConfiguration apiConfiguration)
        {
            builder.Services
                .AddAutoMapper(typeof(Program).Assembly)
                .AddLogging()
                .AddHackSystemObserver()
                .AddBlazoredLocalStorage()
                .AddHackSystemProgramScheduler(options =>
                {
                    options.ProgramLayerStart = 200;
                    options.TopProgramLayerStart = 850;
                })
                .AddCookieStorage()
                .AddAuthorizationCore()
                .AddHackSystemAuthentication(options =>
                {
                    _ = options.AnonymousState.User.Claims.Append(new Claim(ClaimTypes.Name, "Anonymous"));
                    options.AuthenticationURL = apiConfiguration.APIURL;
                    options.TokenExpiryInMinutes = apiConfiguration.TokenExpiryInMinutes;
                    options.AuthenticationScheme = WebCommonSense.AuthenticationScheme;
                    options.AuthenticationType = WebCommonSense.AuthenticationType;
                    options.AuthTokenName = WebCommonSense.AuthTokenName;
                    options.ExpiryClaimType = WebCommonSense.ExpiryClaimType;
                    options.TokenRefreshInMinutes = apiConfiguration.TokenRefreshInMinutes;
                })
                .AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiConfiguration.APIURL) })
                .AddHttpClient("LocalHttpClient", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            return builder;
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebAssemblyHostBuilder InitializeConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.Configure<APIConfiguration>(builder.Configuration.GetSection("APIConfiguration"));

            return builder;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <remarks>可以有更优雅的方式外置这些代码，需要注意 Service 需要的 Options 的传递</remarks>
        public static WebAssemblyHostBuilder InitializeHackSystemServices(this WebAssemblyHostBuilder builder, APIConfiguration apiConfiguration)
        {
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(apiConfiguration.APIURL));
            builder.Services.AddHttpClient<IBasicProgramService, BasicProgramService>(httpClient => httpClient.BaseAddress = new Uri(apiConfiguration.APIURL));

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
        public static WebAssemblyHostBuilder InitializeAuthorizationPolicy(this WebAssemblyHostBuilder builder)
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
