using HackSystem.WebAPI.MockServer.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace HackSystem.WebAPI.MockServer.Application.Wrappers;

public interface IMockRouteResponseWrapper
{
    void WrapMockResponse(HttpContext context, MockRouteDetail mockRoute, in string requestContent, out string responseContent);
}
