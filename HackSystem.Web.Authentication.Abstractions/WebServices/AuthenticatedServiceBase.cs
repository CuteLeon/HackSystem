namespace HackSystem.Web.Authentication.WebServices;

public class AuthenticatedServiceBase
{
    protected readonly ILogger<AuthenticatedServiceBase> logger;
    protected readonly AuthenticatedHttpClient httpClient;

    public AuthenticatedServiceBase(
        ILogger<AuthenticatedServiceBase> logger,
        AuthenticatedHttpClient httpClient)
    {
        this.logger = logger;
        this.httpClient = httpClient;
    }
}
