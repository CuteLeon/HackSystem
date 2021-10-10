using Bunit;
using HackSystem.DataTransferObjects.Accounts;
using HackSystem.Web.Application.Authentication;
using HackSystem.Web.Authentication.AuthorizationStateHandlers;
using HackSystem.Web.Authentication.TokenHandlers;
using HackSystem.Web.CookieStorage;
using HackSystem.WebTests.Mocks;
using Microsoft.AspNetCore.Components;
using Moq;
using Xunit;

namespace HackSystem.Web.Account.Tests;

/// <summary>
/// <see cref="https://bunit.egilhansen.com/docs/getting-started/index.html"/>
/// </summary>
public class LoginComponentTests
{
    [Theory]
    [InlineData("Can not connect to server.", false, false)]
    [InlineData("Login failed!", true, false)]
    [InlineData("<Fake Token>", true, true)]
    public void LoginComponentTest(string message, bool connection, bool loginResult)
    {
        Uri baseUri = new("https://localhost:4237/");
        Uri logoutUri = new(baseUri, "/api/accounts/login");
        var mockAuthenticationService = new Mock<IAuthenticationService>();
        mockAuthenticationService.Setup(x => x.Login(It.IsAny<LoginRequest>())).ReturnsAsync(() =>
        {
            if (!connection)
            {
                throw new HttpRequestException(message);
            }
            var response = new LoginResponse()
            {
                Successful = loginResult,
                Error = loginResult ? string.Empty : message,
                Token = loginResult ? message : string.Empty,
            };
            return response;
        });
        var mockNavigationManager = new MockNavigationManager(baseUri.AbsoluteUri, logoutUri.AbsoluteUri);
        var mockTokenHandler = new Mock<IHackSystemAuthenticationTokenHandler>();
        var mockStateHandler = new Mock<IHackSystemAuthenticationStateUpdater>();

        using var ctx = new TestContext();
        ctx.Services
            .AddLogging()
            .AddSingleton<NavigationManager>(mockNavigationManager)
            .AddSingleton(new Mock<ICookieStorageHandler>().Object)
            .AddSingleton(mockAuthenticationService.Object)
            .AddSingleton(mockTokenHandler.Object)
            .AddSingleton(mockStateHandler.Object);

        using var loginComponent = ctx.RenderComponent<LoginComponent>();
        loginComponent.SaveSnapshot();

        var userNameInput = loginComponent.Find("#userName");
        var passwordInput = loginComponent.Find("#password");
        var loginForm = loginComponent.Find("#loginForm");
        userNameInput.Change("Leon");
        passwordInput.Change("Password");
        loginForm.Submit();

        loginComponent.WaitForState(() => !loginComponent.Instance.Logging || mockNavigationManager.HasNavigated, TimeSpan.FromSeconds(5));
        if (loginResult)
        {
            Assert.True(mockNavigationManager.HasNavigated);
            Assert.EndsWith("/Desktop", mockNavigationManager.Uri, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            Assert.False(mockNavigationManager.HasNavigated);
            var errorAlert = loginComponent.Find("#errorAlert");
            Assert.NotNull(errorAlert);
            errorAlert.MarkupMatches($"<div diff:ignore><strong diff:ignore/><button diff:ignore/>{message}</div>");
        }

        var diff = loginComponent.GetChangesSinceSnapshot();
    }
}
