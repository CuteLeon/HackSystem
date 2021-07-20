using HackSystem.WebAPI.Model.Mock;
using Microsoft.AspNetCore.Http;

namespace HackSystem.WebAPI.MockServer.Services
{
    public class MockRouteResponseWrapper : IMockRouteResponseWrapper
    {
        public void WrapMockResponse(HttpContext context, MockRouteDetail mockRoute, in string requestContent, out string responseContent)
        {
            context.Response.StatusCode = mockRoute.StatusCode;

            responseContent = string.Empty;
            if (mockRoute.MockType == MockType.GenerateByTemplate)
            {
                responseContent = mockRoute.ResponseBodyTemplate;
            }
            else if (mockRoute.MockType == MockType.ReadFromPayload)
            {
                responseContent = requestContent;
            }
            context.Response.WriteAsync(responseContent).ConfigureAwait(false);
        }
    }
}
