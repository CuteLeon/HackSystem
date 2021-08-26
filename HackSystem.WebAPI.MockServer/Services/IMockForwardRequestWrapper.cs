using HackSystem.WebAPI.Model.Mock;
using Microsoft.AspNetCore.Http;

namespace HackSystem.WebAPI.MockServer.Services;

public interface IMockForwardRequestWrapper
{
    HttpRequestMessage WrapForwardRequest(HttpContext context, MockRouteDetail mockRoute, in string requestContent, out string forwardRequestContent);
}
