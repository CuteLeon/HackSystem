﻿using System.Net;
using Bunit;
using HackSystem.Web.Account;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Application.Authentication;
using HackSystem.Web.Infrastructure.Authentication;
using HackSystem.WebTests.Mocks;
using Microsoft.AspNetCore.Components;
using Moq;
using Moq.Contrib.HttpClient;
using Xunit;
using HackSystem.Intermediary.Extensions;
using HackSystem.Intermediary.Application;
using HackSystem.Web.Domain.Intermediary;

namespace HackSystem.WebTests.Account;

public class LogoutComponentTests
{
    [Fact()]
    public void LogoutComponentTest()
    {
        Uri baseUri = new("https://localhost:4237/");
        Uri logoutUri = new(baseUri, "/api/accounts/logout");
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var mockLogoutCommandHandler = new Mock<IIntermediaryCommandHandler<LogoutCommand>>();
        var mockHttpClient = mockHttpMessageHandler.CreateClient();
        mockHttpMessageHandler.SetupAnyRequest().Throws(new HttpRequestException("Not allowed request than testing request."));
        mockHttpMessageHandler.SetupRequest(HttpMethod.Get, logoutUri).ReturnsResponse(HttpStatusCode.OK, "Logout successfully.");
        mockHttpClient.BaseAddress = baseUri;
        var mockNavigationManager = new MockNavigationManager(baseUri.AbsoluteUri, logoutUri.AbsoluteUri);

        using var ctx = new TestContext();
        ctx.Services
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryCommandHandlerSingleton<IIntermediaryCommandHandler<LogoutCommand>, LogoutCommand>(mockLogoutCommandHandler.Object)
            .AddSingleton<NavigationManager>(mockNavigationManager)
            .AddSingleton(new Mock<ICookieStorageHandler>().Object)
            .AddHackSystemAuthentication(options =>
            {
                options.AuthenticationURL = baseUri.AbsoluteUri;
            })
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddSingleton(mockHttpClient);

        using var logoutComponent = ctx.RenderComponent<LogoutComponent>();
        logoutComponent.WaitForState(
            () => mockNavigationManager.HasNavigated, TimeSpan.FromSeconds(5));
        Assert.EndsWith("/StartUp", mockNavigationManager.Uri, StringComparison.OrdinalIgnoreCase);
    }
}
