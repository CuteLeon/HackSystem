﻿using HackSystem.Web.Authentication.AuthorizationStateHandlers;
using HackSystem.Web.Authentication.ClaimsIdentityHandlers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.Authentication.WebServices;
using Microsoft.AspNetCore.Components.Authorization;

namespace HackSystem.Web.Authentication.Extensions;

public static class HackSystemAuthenticationExtension
{
    public static IServiceCollection AddHackSystemAuthentication(
        this IServiceCollection services,
        Action<HackSystemAuthenticationOptions> configure)
    {
        services
            .Configure(configure)
            //.AddScoped<IAuthorizationService, HackSystemAuthorizationService>()
            //.AddScoped<IAuthorizationHandlerContextFactory, HackSystemAuthorizationHandlerContextFactory>()
            .AddScoped<IJsonWebTokenParser, JsonWebTokenParser>()
            .AddScoped<IHackSystemClaimsIdentityValidator, HackSystemClaimsIdentityValidator>()
            .AddScoped<IHackSystemAuthenticationTokenHandler, HackSystemAuthenticationTokenHandler>()
            .AddScoped<AuthenticationStateProvider, HackSystemAuthenticationStateProvider>()
            .AddScoped<IHackSystemAuthenticationStateUpdater, HackSystemAuthenticationStateUpdater>()
            .AddSingleton<IHackSystemAuthenticationTokenRefresher, HackSystemAuthenticationTokenRefresher>()
            .AddTransient<AuthenticatedDelegatingHandler>()
            .AddHttpClient(AuthenticatedServiceBase.AuthenticatedClientName)
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<HackSystemAuthenticationOptions>>().Value;
                client.BaseAddress = new Uri(options.AuthenticationURL);
            })
            .AddHttpMessageHandler<AuthenticatedDelegatingHandler>();

        return services;
    }
}
