using System.Net.Http.Headers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;

namespace HackSystem.Web.Authentication.WebServices;

/// <summary>
/// Authenticated http client
/// </summary>
/// <remarks>
/// Can only add authenticated header for Post request automatically.
/// https://github.com/dotnet/runtime/issues/23322
/// </remarks>
public class AuthenticatedHttpClient : HttpClient
{
    private readonly IHackSystemAuthenticationTokenHandler authenticationTokenHandler;
    private readonly HackSystemAuthenticationOptions options;

    public AuthenticatedHttpClient(
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        IOptionsSnapshot<HackSystemAuthenticationOptions> optionsSnapshot)
    {
        this.authenticationTokenHandler = authenticationTokenHandler;
        this.options = optionsSnapshot.Value;
        this.BaseAddress = new Uri(this.options.AuthenticationURL);
    }

    public async Task AddAuthorizationHeaderAsync()
    {
        var token = await this.authenticationTokenHandler.GetTokenAsync();
        this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.options.AuthenticationScheme, token);
    }


    public override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        this.AddAuthorizationHeaderAsync().ConfigureAwait(false);
        return base.Send(request, cancellationToken);
    }

    public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await this.AddAuthorizationHeaderAsync();
        return await base.SendAsync(request, cancellationToken);
    }
}
