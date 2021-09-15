using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Bunit;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.Authentication;
using HackSystem.WebDataTransfer.Account;
using HackSystem.WebTests.Mocks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Contrib.HttpClient;
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
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.SetupAnyRequest().Throws(new HttpRequestException(message));
        mockHttpMessageHandler.SetupRequest(HttpMethod.Post, logoutUri).ReturnsAsync(() =>
        {
            if (!connection)
            {
                throw new HttpRequestException(message);
            }
            var responseJson = JsonSerializer.Serialize(new LoginResultDTO()
            {
                Successful = loginResult,
                Error = loginResult ? string.Empty : message,
                Token = loginResult ? message : string.Empty,
            });
            var response = new HttpResponseMessage
            {
                StatusCode = loginResult ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Content = new StringContent(responseJson),
            };
            return response;
        });
        var mockHttpClient = mockHttpMessageHandler.CreateClient();
        mockHttpClient.BaseAddress = baseUri;
        var mockNavigationManager = new MockNavigationManager(baseUri.AbsoluteUri, logoutUri.AbsoluteUri);
        var mockHackSystemAuthenticationStateHandler = new Mock<IHackSystemAuthenticationStateHandler>();

        using var ctx = new TestContext();
        ctx.Services
            .AddLogging()
            .AddSingleton<NavigationManager>(mockNavigationManager)
            .AddSingleton(new Mock<ICookieStorageService>().Object)
            .AddSingleton(mockHackSystemAuthenticationStateHandler.Object)
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddSingleton(mockHttpClient);

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
