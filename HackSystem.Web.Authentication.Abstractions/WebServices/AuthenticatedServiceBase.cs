using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Options;
using HackSystem.Web.Authentication.TokenHandlers;

namespace HackSystem.Web.Authentication.WebService;

public class AuthenticatedServiceBase
{
    protected readonly ILogger<AuthenticatedServiceBase> logger;
    protected readonly IHackSystemAuthenticationTokenHandler authenticationTokenHandler;
    protected readonly HackSystemAuthenticationOptions authenticationOptions;
    protected readonly HttpClient httpClient;

    public AuthenticatedServiceBase(
        ILogger<AuthenticatedServiceBase> logger,
        IHackSystemAuthenticationTokenHandler authenticationTokenHandler,
        IOptionsSnapshot<HackSystemAuthenticationOptions> optionsSnapshot,
        HttpClient httpClient)
    {
        this.logger = logger;
        this.authenticationTokenHandler = authenticationTokenHandler;
        this.authenticationOptions = optionsSnapshot.Value;
        this.httpClient = httpClient;
    }

    protected async Task AddAuthorizationHeaderAsync()
    {
        this.httpClient.AddAuthorizationHeader(
            authenticationOptions.AuthenticationScheme,
            await this.authenticationTokenHandler.GetTokenAsync());
    }
}
