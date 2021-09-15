using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.Services;
using HackSystem.Web.CookieStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace HackSystem.Web.Authentication.Providers.Tests;

public class HackSystemAuthenticationStateProviderTests
{
    [Theory]
    [InlineData(new[] { ClaimTypes.Name, "exp" }, new[] { "Leon", "253400000800" }, true)]
    [InlineData(new[] { ClaimTypes.Name, "exp" }, new[] { "Leon", "1" }, false)]
    public void CheckClaimsIdentityTest(string[] claimTypes, string[] claimParameters, bool expectedValidity)
    {
        var claimIdentity = new ClaimsIdentity(claimTypes.Zip(claimParameters, (type, parameter) => new Claim(type, parameter)));
        var options = new HackSystemAuthenticationOptions();
        var mockLogger = new Mock<ILogger<HackSystemAuthenticationStateProvider>>();
        var mockOptions = new Mock<IOptionsMonitor<HackSystemAuthenticationOptions>>();
        mockOptions.SetupGet(m => m.CurrentValue).Returns(options);
        var jwtParserService = new JWTParserService(new Mock<ILogger<JWTParserService>>().Object);
        var mockCookieStorageService = new Mock<ICookieStorageService>();

        var serviceInstance = new HackSystemAuthenticationStateProvider(
            mockLogger.Object,
            mockOptions.Object,
            jwtParserService,
            mockCookieStorageService.Object);
        var validity = serviceInstance.CheckClaimsIdentity(claimIdentity);

        Assert.Equal(expectedValidity, validity);
    }

    [Theory]
    [InlineData(null, false, null)]
    [InlineData("", false, null)]
    [InlineData("   ", false, null)]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOlsidHJ1ZSIsInRydWUiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjE2MTM0NTM3OTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.lHgI8ZGPFMnqgcMD28RYCwtsAVT3PA5jH4gdd-CykVI", false, null)]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOlsidHJ1ZSIsInRydWUiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjI1MzQwMjI3MjAwMCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3QiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdCJ9.EP_SSsQjTXM7Bl1QoqIirJTn8B2am5rVZLmyzYBjKIE", true, "jwt")]
    public async Task GetAuthenticationStateAsyncTest(string authToken, bool isAuthenticated, string authenticationType)
    {
        var options = new HackSystemAuthenticationOptions();
        var mockLogger = new Mock<ILogger<HackSystemAuthenticationStateProvider>>();
        var mockOptions = new Mock<IOptionsMonitor<HackSystemAuthenticationOptions>>();
        mockOptions.SetupGet(m => m.CurrentValue).Returns(options);
        var jwtParserService = new JWTParserService(new Mock<ILogger<JWTParserService>>().Object);
        var mockCookieStorageService = new Mock<ICookieStorageService>();
        mockCookieStorageService.Setup(m => m.GetCookieAsync(It.Is<string>(s => s.Equals(options.AuthTokenName)))).ReturnsAsync(authToken);

        AuthenticationStateProvider serviceInstance = new HackSystemAuthenticationStateProvider(
            mockLogger.Object,
            mockOptions.Object,
            jwtParserService,
            mockCookieStorageService.Object);
        var authState = await serviceInstance.GetAuthenticationStateAsync();

        Assert.Equal(authenticationType, authState.User.Identity.AuthenticationType);
        Assert.Equal(isAuthenticated, authState.User.Identity.IsAuthenticated);

        if (!isAuthenticated)
        {
            Assert.Same(options.AnonymousState, authState);
        }
    }
}
