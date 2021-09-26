using System.Security.Claims;

namespace HackSystem.Web.Authentication.TokenHandlers;

public interface IJsonWebTokenParser
{
    IEnumerable<Claim> ParseJWTToken(string token);

    IEnumerable<Claim> ParseJWTPayload(string token);
}
