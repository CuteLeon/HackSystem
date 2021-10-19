namespace HackSystem.Web.Authentication.WebServices;

public class AuthenticatedServiceBase
{
    public const string AuthenticatedClientName = "AuthenticatedClient";
    private readonly IHttpClientFactory httpClientFactory;
    protected readonly ILogger<AuthenticatedServiceBase> logger;

    protected HttpClient HttpClient { get; init; }

    public AuthenticatedServiceBase(
        ILogger<AuthenticatedServiceBase> logger,
        IHttpClientFactory httpClientFactory)
    {
        this.logger = logger;
        this.httpClientFactory = httpClientFactory;
        this.HttpClient = this.httpClientFactory.CreateClient(AuthenticatedClientName);
    }
}
