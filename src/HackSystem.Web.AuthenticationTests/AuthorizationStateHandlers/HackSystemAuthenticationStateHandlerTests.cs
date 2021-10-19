using HackSystem.Web.Authentication.ClaimsIdentityHandlers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;
using Moq;
using Xunit;

namespace HackSystem.Web.Authentication.AuthorizationStateHandlers.Tests;

public class HackSystemAuthenticationStateHandlerTests
{
    [Theory]
    [InlineData(null, false, null)]
    [InlineData("", false, null)]
    [InlineData("   ", false, null)]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOlsidHJ1ZSIsInRydWUiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjE2MTM0NTM3OTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.lHgI8ZGPFMnqgcMD28RYCwtsAVT3PA5jH4gdd-CykVI", false, null)]
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOlsidHJ1ZSIsInRydWUiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjI1MzQwMjI3MjAwMCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3QiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdCJ9.EP_SSsQjTXM7Bl1QoqIirJTn8B2am5rVZLmyzYBjKIE", true, "jwt")]
    public async Task GetAuthenticationStateAsyncTest(string authToken, bool isAuthenticated, string authenticationType)
    {
        var options = new HackSystemAuthenticationOptions();
        var mockOptions = new Mock<IOptionsSnapshot<HackSystemAuthenticationOptions>>();
        mockOptions.SetupGet(m => m.Value).Returns(options);

        var mockLogger = new Mock<ILogger<HackSystemAuthenticationStateProvider>>();
        var jwtParserService = new JsonWebTokenParser(new Mock<ILogger<JsonWebTokenParser>>().Object);
        var claimsIdentityValidator = new HackSystemClaimsIdentityValidator(new Mock<ILogger<HackSystemClaimsIdentityValidator>>().Object, mockOptions.Object);
        var mockTokenHandler = new Mock<IHackSystemAuthenticationTokenHandler>();
        mockTokenHandler.Setup(m => m.GetTokenAsync()).ReturnsAsync(authToken);

        IHackSystemAuthenticationStateProvider serviceInstance = new HackSystemAuthenticationStateProvider(
            mockLogger.Object,
            mockOptions.Object,
            mockTokenHandler.Object,
            jwtParserService,
            claimsIdentityValidator);
        var authState = await serviceInstance.GetAuthenticationStateAsync();

        Assert.Equal(authenticationType, authState.User.Identity.AuthenticationType);
        Assert.Equal(isAuthenticated, authState.User.Identity.IsAuthenticated);

        if (!isAuthenticated)
        {
            Assert.Same(options.AnonymousState, authState);
        }
    }
}
