using System.Net.Http;
using HackSystem.Web.Authentication.Providers;
using HackSystem.Web.Services.API.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Services.Program
{
    public class BasicProgramService : IBasicProgramService
    {
        private readonly ILogger<BasicProgramService> logger;
        private readonly IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler;
        private readonly HttpClient httpClient;

        public BasicProgramService(
            ILogger<BasicProgramService> logger,
            IHackSystemAuthenticationStateHandler hackSystemAuthenticationStateHandler,
            HttpClient httpClient)
        {
            this.logger = logger;
            this.hackSystemAuthenticationStateHandler = hackSystemAuthenticationStateHandler;
            this.httpClient = httpClient;
        }
    }
}
