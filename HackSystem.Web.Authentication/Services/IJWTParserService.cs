using System.Security.Claims;

namespace HackSystem.Web.Authentication.Services;

public interface IJWTParserService
{
    IEnumerable<Claim> ParseJWTToken(string token);

    IEnumerable<Claim> ParseJWTPayload(string token);
}
