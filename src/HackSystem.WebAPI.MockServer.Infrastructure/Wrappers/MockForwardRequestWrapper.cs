using HackSystem.WebAPI.MockServer.Application.Wrappers;
using HackSystem.WebAPI.MockServer.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace HackSystem.WebAPI.MockServer.Infrastructure.Wrappers;

public class MockForwardRequestWrapper : IMockForwardRequestWrapper
{
    public HttpRequestMessage WrapForwardRequest(HttpContext context, MockRouteDetail mockRoute, in string requestContent, out string forwardRequestContent)
    {
        forwardRequestContent = string.Empty;
        if (mockRoute.ForwardMockType == MockType.GenerateByTemplate)
        {
            forwardRequestContent = mockRoute.ForwardRequestBodyTemplate;
        }
        else if (mockRoute.ForwardMockType == MockType.ReadFromPayload)
        {
            forwardRequestContent = requestContent;
        }

        var request = new HttpRequestMessage()
        {
            Method = new HttpMethod(mockRoute.ForwardMethod),
            RequestUri = new Uri(mockRoute.ForwardAddress),
            Content = new StringContent(forwardRequestContent),
        };
        return request;
    }
}
