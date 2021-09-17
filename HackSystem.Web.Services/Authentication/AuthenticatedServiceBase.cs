using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.Extensions;

namespace HackSystem.Web.Services.Authentication;

public class AuthenticatedServiceBase
{
    protected readonly ILogger logger;
    protected readonly IHackSystemAuthenticationStateProvider hackSystemAuthenticationStateProvider;
    protected readonly HttpClient httpClient;

    public AuthenticatedServiceBase(
        ILogger logger,
        IHackSystemAuthenticationStateProvider hackSystemAuthenticationStateHandler,
        HttpClient httpClient)
    {
        this.logger = logger;
        this.hackSystemAuthenticationStateProvider = hackSystemAuthenticationStateHandler;
        this.httpClient = httpClient;
    }

    protected async Task AddAuthorizationHeaderAsync()
    {
        this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateProvider.GetCurrentTokenAsync());
    }
}
