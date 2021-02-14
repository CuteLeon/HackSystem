using Xunit;
using Bunit;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.API.Authentication;
using HackSystem.Web.Services.Authentication;
using Microsoft.Extensions.Options;
using HackSystem.Web.Authentication.Options;

namespace HackSystem.Web.Account.Tests
{
    public class LoginComponentTests
    {
        [Fact()]
        public void LoginComponentTest()
        {
            using var ctx = new TestContext();
            ctx.Services.Add(new ServiceDescriptor(typeof(ILogger<>), typeof(Logger<>), ServiceLifetime.Scoped));
            ctx.Services.Add(new ServiceDescriptor(typeof(IHackSystemAuthenticationStateHandler), typeof(HackSystemAuthenticationStateHandler), ServiceLifetime.Scoped));
            ctx.Services.Add(new ServiceDescriptor(typeof(IAuthenticationService), typeof(AuthenticationService), ServiceLifetime.Scoped));
            ctx.Services.Add(new ServiceDescriptor(typeof(IOptionsMonitor<>), typeof(OptionsMonitor<>), ServiceLifetime.Scoped));
            ctx.Services.Add(new ServiceDescriptor(typeof(IOptionsFactory<>), typeof(OptionsFactory<>), ServiceLifetime.Scoped));
            ctx.Services.Add(new ServiceDescriptor(typeof(IPostConfigureOptions<>), typeof(PostConfigureOptions<>), ServiceLifetime.Scoped));
            ctx.Services.Add(new ServiceDescriptor(typeof(IOptionsChangeTokenSource<>), null));
            ctx.Services.Add(new ServiceDescriptor(typeof(IOptionsMonitorCache<>), null));

            ctx.Services.Add(new ServiceDescriptor(typeof(IConfigureOptions<>), typeof(ConfigureOptions<>), ServiceLifetime.Scoped));

            var loginComponent = ctx.RenderComponent<LoginComponent>();
            var userNameInput = loginComponent.Find("#userName");
            var passwordInput = loginComponent.Find("#password");
            var loginButton = loginComponent.Find("#loginButton");

            userNameInput.Change("IamLeon");
            passwordInput.Change("FakePassword");

            loginButton.Click();
            var loginComponentBackUp = ctx.RenderComponent<LoginComponent>();
            var diff = loginComponent.CompareTo(loginComponentBackUp);
            //loginComponent.WaitForState();
        }
    }
}