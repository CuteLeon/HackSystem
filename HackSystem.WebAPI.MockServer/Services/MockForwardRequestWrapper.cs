using System;
using System.Net.Http;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.AspNetCore.Http;

namespace HackSystem.WebAPI.MockServer.Services;

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
