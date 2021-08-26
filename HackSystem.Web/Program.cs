using Blazored.LocalStorage;
using HackSystem.Common;
using HackSystem.Cryptography;
using HackSystem.Observer;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Common;
using HackSystem.Web.Configurations;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Extensions;
using HackSystem.Web.Scheduler.Program;
using HackSystem.Web.Services.Configurations;
using HackSystem.Web.Services.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.Configuration
            .AddJsonFile("appsettings.json", true, false)
            .AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", true, false);
        builder.RootComponents.Add<App>("#app");

        builder
            .InitializeBasicService()
            .InitializeAuthorizationPolicy();

        await builder
            .Build()
            .LaunchBasicServices()
            .RunAsync();
    }

    public static WebAssemblyHostBuilder InitializeBasicService(this WebAssemblyHostBuilder builder)
    {
        var apiConfiguration = builder.Configuration.GetSection("APIConfiguration").Get<APIConfiguration>();
        var securityConfiguration = builder.Configuration.GetSection("SecurityConfiguration").Get<SecurityConfiguration>();
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
            .AddRSACryptography(options =>
            {
                options.RSAKeyParameters = securityConfiguration.RSAPublicKey;
            })
            .AddWebServices(new WebServiceOptions()
            {
                APIHost = apiConfiguration.APIHost,
            })
            .AddCookieStorage()
            .AddAuthorizationCore()
            .AddHackSystemAuthentication(options =>
            {
                options.AuthenticationURL = apiConfiguration.APIHost;
                options.TokenExpiryInMinutes = apiConfiguration.TokenExpiryInMinutes;
                options.AuthenticationScheme = WebCommonSense.AuthenticationScheme;
                options.AuthenticationType = WebCommonSense.AuthenticationType;
                options.AuthTokenName = WebCommonSense.AuthTokenName;
                options.ExpiryClaimType = WebCommonSense.ExpiryClaimType;
                options.TokenRefreshInMinutes = apiConfiguration.TokenRefreshInMinutes;
            })
            .AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiConfiguration.APIHost) })
            .AddHttpClient("LocalHttpClient", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

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
