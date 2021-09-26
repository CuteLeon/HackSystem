using System.Security.Claims;
using HackSystem.Web.Authentication.Options;
using Moq;
using Xunit;

namespace HackSystem.Web.Authentication.ClaimsIdentityHandlers.Tests;

public class HackSystemClaimsIdentityValidatorTests
{
    [Theory]
    [InlineData(new[] { ClaimTypes.Name, "exp" }, new[] { "Leon", "253400000800" }, true)]
    [InlineData(new[] { ClaimTypes.Name, "exp" }, new[] { "Leon", "1" }, false)]
    public void ValidateClaimsIdentityTest(string[] claimTypes, string[] claimParameters, bool expectedValidity)
    {
        var claimIdentity = new ClaimsIdentity(claimTypes.Zip(claimParameters, (type, parameter) => new Claim(type, parameter)));
        var options = new HackSystemAuthenticationOptions();
        var mockLogger = new Mock<ILogger<HackSystemClaimsIdentityValidator>>();
        var mockOptions = new Mock<IOptionsSnapshot<HackSystemAuthenticationOptions>>();
        mockOptions.SetupGet(m => m.Value).Returns(options);

        var serviceInstance = new HackSystemClaimsIdentityValidator(
            mockLogger.Object,
            mockOptions.Object);
        var validity = serviceInstance.ValidateClaimsIdentity(claimIdentity);

        Assert.Equal(expectedValidity, validity);
    }
}
