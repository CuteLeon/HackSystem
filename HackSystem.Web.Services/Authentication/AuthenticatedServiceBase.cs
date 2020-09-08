using System.Net.Http;
using System.Threading.Tasks;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.Extensions;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Services.Authentication
{
    public class AuthenticatedServiceBase
    {
        protected readonly ILogger logger;
        protected readonly IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler;
        protected readonly HttpClient httpClient;

        public AuthenticatedServiceBase(
            ILogger logger,
            IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler,
            HttpClient httpClient)
        {
            this.logger = logger;
            this.hackSystemAuthenticationStateHandler = hackSystemAuthenticationStateHandler;
            this.httpClient = httpClient;
        }

        protected async Task AddAuthorizationHeaderAsync()
        {
            this.httpClient.AddAuthorizationHeader(await hackSystemAuthenticationStateHandler.GetCurrentTokenAsync());
        }
    }
}
