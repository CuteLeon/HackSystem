using System;
using Bunit;
using HackSystem.Web.Account;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HackSystem.WebTests.Account
{
    public class LogoutComponentTests
    {
        [Fact()]
        public void LogoutComponentTest()
        {
            const string AuthenticationURL = "https://localhost:4237";
            using var ctx = new TestContext();
            ctx.Services.AddLogging()
                .AddCookieStorage()
                .AddAuthorizationCore()
                .AddHackSystemAuthentication(options =>
                {
                    options.AuthenticationURL = AuthenticationURL;
                })
                .AddHttpClient<IAuthenticationService, AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(AuthenticationURL));

            using var logoutComponent = ctx.RenderComponent<LogoutComponent>();
        }
    }
}
