using System.Net.Http;
using System.Net.Http.Headers;

namespace HackSystem.Web.Authentication.Extensions;

    public static class HttpClientExtension
    {
        public static HttpClient AddAuthorizationHeader(this HttpClient httpClient, string authenticationScheme, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationScheme, token);
            return httpClient;
        }
    }
