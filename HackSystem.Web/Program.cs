using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using HackSystem.Common;
using HackSystem.Observer;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Common;
using HackSystem.Web.Configurations;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Extensions;
using HackSystem.Web.Scheduler.Program;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.API.Program;
using HackSystem.Web.Services.Authentication;
using HackSystem.Web.Services.Program;
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
        /// Initialize Configuration
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebAssemblyHostBuilder InitializeConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.Configure<APIConfiguration>(builder.Configuration.GetSection("APIConfiguration"));

            return builder;
        }

        /// <summary>
        /// Initialize Services
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebAssemblyHostBuilder InitializeHackSystemServices(this WebAssemblyHostBuilder builder, APIConfiguration apiConfiguration)
        {
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(apiConfiguration.APIURL));
            builder.Services.AddHttpClient<IBasicProgramService, BasicProgramService>(httpClient => httpClient.BaseAddress = new Uri(apiConfiguration.APIURL));

            return builder;
        }

        /// <summary>
        /// Initialize Authorization Policy
        /// </summary>
        /// <param name="builder"></param>
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
