using System;
using System.Net.Http;
using HackSystem.Observer;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.ProgramDevelopmentKit;
using HackSystem.Web.ProgramDevelopmentKit.Authentication;
using HackSystem.Web.Services.Configurations;
using HackSystem.Web.Services.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
    .AddScoped<AuthenticationStateProvider, DevelopmentKitAuthenticationStateProvider>()
    .AddTransient(sp => new HttpClient { BaseAddress = new Uri(apiHost) })
    .AddHttpClient("LocalHttpClient", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();