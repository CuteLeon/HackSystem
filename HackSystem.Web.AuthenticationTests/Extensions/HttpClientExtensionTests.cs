using System.Net.Http;
using HackSystem.Web.Authentication.Options;
using Xunit;

namespace HackSystem.Web.Authentication.Extensions.Tests;

public class HttpClientExtensionTests
{
    [Fact()]
    public void AddAuthorizationHeaderTest()
    {
        var options = new HackSystemAuthenticationOptions();
        var currentToken = "Current valid token.";
        var httpClient = new HttpClient();
        httpClient.AddAuthorizationHeader(options.AuthenticationScheme, currentToken);

        Assert.Equal(options.AuthenticationScheme, httpClient.DefaultRequestHeaders.Authorization.Scheme);
        Assert.Equal(currentToken, httpClient.DefaultRequestHeaders.Authorization.Parameter);
    }
}
