using System;
using Bunit;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.CookieStorage;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HackSystem.Web.Account.Tests
{
    public class LoginComponentTests
    {
        [Fact()]
        public void LoginComponentTest()
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
                .AddHttpClient<IAuthenticationService, AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(AuthenticationURL)); ;

            var loginComponent = ctx.RenderComponent<LoginComponent>();
            var userNameInput = loginComponent.Find("#userName");
            var passwordInput = loginComponent.Find("#password");
            var loginForm = loginComponent.Find("#loginForm");

            userNameInput.Change("IamLeon");
            passwordInput.Change("FakePassword");
            loginForm.Submit();

            var loginComponentBackUp = ctx.RenderComponent<LoginComponent>();
            var diff = loginComponent.CompareTo(loginComponentBackUp);
        }
    }
}