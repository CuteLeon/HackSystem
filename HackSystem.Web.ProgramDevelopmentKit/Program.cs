using HackSystem.Observer;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Common;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.ProgramDevelopmentKit;
using HackSystem.Web.Services.Configurations;
using HackSystem.Web.Services.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var apiHost = "https://localhost:4237";
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services
    .AddLogging()
    .AddHackSystemObserver()
    .AddWebServices(new WebServiceOptions()
    {
        APIHost = apiHost,
    })
    .AddCookieStorage()
    .AddAuthorizationCore()
    .AddHackSystemAuthentication(options =>
    {
        options.AuthenticationURL = apiHost;
        options.TokenExpiryInMinutes = int.MaxValue;
        options.AuthenticationScheme = WebCommonSense.AuthenticationScheme;
        options.AuthenticationType = WebCommonSense.AuthenticationType;
        options.AuthTokenName = WebCommonSense.AuthTokenName;
        options.ExpiryClaimType = WebCommonSense.ExpiryClaimType;
        options.TokenRefreshInMinutes = int.MaxValue;
    })
    .AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiHost) })
    .AddHttpClient("LocalHttpClient", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();