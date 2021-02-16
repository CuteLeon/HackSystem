using System;
using System.Security.Claims;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.CookieStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Moq;
using Xunit;

namespace HackSystem.Web.Authentication.Extensions.Tests
{
    public class HackSystemAuthenticationExtensionTests
    {
        [Fact()]
        public void AddHackSystemAuthenticationTest()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            var mockJSService = new Mock<IJSRuntime>();
            serviceDescriptors
                .AddLogging()
                .AddCookieStorage()
                .AddHttpClient()
                .AddSingleton(mockJSService.Object)
                .AddHackSystemAuthentication(options =>
                {
                    options.AuthenticationURL = "https://FakeAuth.url";
                });
            IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();

            var optionsInstance = serviceProvider.GetRequiredService<IOptions<HackSystemAuthenticationOptions>>();
            Assert.NotNull(optionsInstance);
            Assert.NotNull(optionsInstance.Value);
            Assert.Equal("AuthToken", optionsInstance.Value.AuthTokenName);
            Assert.Equal("jwt", optionsInstance.Value.AuthenticationType);
            Assert.Equal("bearer", optionsInstance.Value.AuthenticationScheme);
            Assert.Equal("exp", optionsInstance.Value.ExpiryClaimType);
            Assert.Equal("https://FakeAuth.url", optionsInstance.Value.AuthenticationURL);
            Assert.Equal(30, optionsInstance.Value.TokenExpiryInMinutes);
            Assert.Equal(25, optionsInstance.Value.TokenRefreshInMinutes);
            Assert.True(optionsInstance.Value.AnonymousState.User.HasClaim(c => c.Type == ClaimTypes.Name && c.Value == "Anonymous"));

            object serviceInstance = serviceProvider.GetRequiredService<IJWTParserService>();
            Assert.NotNull(serviceInstance);
            Assert.IsType<JWTParserService>(serviceInstance);

            serviceInstance = serviceProvider.GetRequiredService<IHackSystemAuthenticationTokenRefresher>();
            Assert.NotNull(serviceInstance);
            Assert.IsType<HackSystemAuthenticationTokenRefresher>(serviceInstance);

            serviceInstance = serviceProvider.GetRequiredService<IHackSystemAuthenticationStateHandler>();
            Assert.NotNull(serviceInstance);
            Assert.IsType<HackSystemAuthenticationStateHandler>(serviceInstance);

            serviceInstance = serviceProvider.GetRequiredService<AuthenticationStateProvider>();
            Assert.NotNull(serviceInstance);
            Assert.IsType<HackSystemAuthenticationStateProvider>(serviceInstance);
        }
    }
}