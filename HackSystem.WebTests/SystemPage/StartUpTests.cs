using Bunit;
using Bunit.TestDoubles;
using HackSystem.Web.SystemPage;
using HackSystem.WebTests.Mocks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HackSystem.WebTests.SystemPage;

public class StartUpTests
{
    [Fact()]
    public void StartUpTest()
    {
        Uri baseUri = new("https://localhost:4237/");
        var mockNavigationManager = new MockNavigationManager(baseUri.AbsoluteUri, baseUri.AbsoluteUri);

        using var ctx = new TestContext();
        ctx.Services
            .AddLogging()
            .AddSingleton<NavigationManager>(mockNavigationManager);
        var authorizationContext = ctx.AddTestAuthorization();

        using var startupNoAuthorization = ctx.RenderComponent<StartUp>();
        startupNoAuthorization.WaitForState(() => mockNavigationManager.HasNavigated, TimeSpan.FromSeconds(5));
        Assert.EndsWith("/Login", mockNavigationManager.Uri, StringComparison.OrdinalIgnoreCase);
        mockNavigationManager.ClearHistory();

        authorizationContext.SetNotAuthorized();
        using var startupNotAuthorized = ctx.RenderComponent<StartUp>();
        startupNotAuthorized.WaitForState(() => mockNavigationManager.HasNavigated, TimeSpan.FromSeconds(5));
        Assert.EndsWith("/Login", mockNavigationManager.Uri, StringComparison.OrdinalIgnoreCase);
        mockNavigationManager.ClearHistory();

        authorizationContext.SetAuthorized("Leon", AuthorizationState.Authorized);
        using var startupAuthorized = ctx.RenderComponent<StartUp>();
        startupAuthorized.WaitForState(() => mockNavigationManager.HasNavigated, TimeSpan.FromSeconds(5));
        Assert.EndsWith("/Desktop", mockNavigationManager.Uri, StringComparison.OrdinalIgnoreCase);
        mockNavigationManager.ClearHistory();

        authorizationContext.SetNotAuthorized();
        using var startupLogout = ctx.RenderComponent<StartUp>();
        startupLogout.WaitForState(() => mockNavigationManager.HasNavigated, TimeSpan.FromSeconds(5));
        Assert.EndsWith("/Login", mockNavigationManager.Uri, StringComparison.OrdinalIgnoreCase);
        mockNavigationManager.ClearHistory();
    }
}
