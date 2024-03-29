﻿using Blazored.LocalStorage;
using HackSystem.Common;
using HackSystem.Cryptography;
using HackSystem.Web;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Common;
using HackSystem.Web.Component.Extensions;
using HackSystem.Web.Configurations;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Domain.Configurations;
using HackSystem.Web.Extensions;
using HackSystem.Web.Infrastructure.Extensions;
using HackSystem.Web.ProgramSchedule;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var env = builder.HostEnvironment;
var config = builder.Configuration;
var apiConfiguration = config.GetSection("APIConfiguration").Get<APIConfiguration>();
var securityConfiguration = config.GetSection("SecurityConfiguration").Get<SecurityConfiguration>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", true, true);

builder.Services
    .Configure<WebComponentTierConfiguration>(config)
    .AddAutoMapper(typeof(Program).Assembly)
    .AddLogging()
    .AddBlazoredLocalStorage()
    .AddHackSystemWebIntermediary()
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
    .AddHackSystemComponent()
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
    .AddAuthorizationCore(options =>
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

builder.Services.AddHttpClient<HttpClient>(client => client.BaseAddress = new Uri(apiConfiguration.APIHost));
builder.Services.AddHttpClient("LocalHttpClient", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

await builder
    .Build()
    .LaunchBasicServices()
    .RunAsync();
