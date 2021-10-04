using HackSystem.Intermediary.Extensions;
using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Domain.Configurations;
using HackSystem.Web.Infrastructure.Extensions;
using HackSystem.Web.ProgramDevelopmentKit;
using HackSystem.Web.ProgramDevelopmentKit.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var apiHost = "https://localhost:4237";
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services
    .AddLogging()
    .AddWebServices(new WebServiceOptions()
    {
        APIHost = apiHost,
    })
    .AddHackSystemIntermediary()
    .AddCookieStorage()
    .AddAuthorizationCore()
    .AddScoped<IHackSystemAuthenticationTokenHandler, DevelopmentKitAuthenticationTokenHandler>()
    .AddScoped<AuthenticationStateProvider, DevelopmentKitAuthenticationStateProvider>()
    .AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiHost) })
    .AddHttpClient("LocalHttpClient", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();