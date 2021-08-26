using HackSystem.WebAPI.Model.Mock;
using Microsoft.AspNetCore.Http;

namespace HackSystem.WebAPI.MockServer.Services;

public interface IMockRouteResponseWrapper
{
    void WrapMockResponse(HttpContext context, MockRouteDetail mockRoute, in string requestContent, out string responseContent);
}
