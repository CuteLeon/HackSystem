using System.Net.Http.Headers;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;

namespace HackSystem.Web.Authentication.WebServices;

public class AuthenticatedDelegatingHandler : DelegatingHandler
{
    private readonly IHackSystemAuthenticationTokenHandler authenticationTokenHandler;
    private readonly HackSystemAuthenticationOptions options;

    public AuthenticatedDelegatingHandler(
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        IOptionsSnapshot<HackSystemAuthenticationOptions> optionsSnapshot)
    {
        this.authenticationTokenHandler = authenticationTokenHandler;
        this.options = optionsSnapshot.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await this.AttachAuthorizationHeader(request);
        return await base.SendAsync(request, cancellationToken);
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        this.AttachAuthorizationHeader(request).ConfigureAwait(false);
        return base.Send(request, cancellationToken);
    }

    protected async Task AttachAuthorizationHeader(HttpRequestMessage request)
    {
        if (request.Headers.Authorization is null)
        {
            var token = await this.authenticationTokenHandler.GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue(this.options.AuthenticationScheme, token);
        }
    }
}
