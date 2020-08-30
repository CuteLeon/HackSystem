using System.Collections.Generic;
using System.Security.Claims;

namespace HackSystem.Web.Authentication.Services
{
    public interface IJWTParser
    {
        IEnumerable<Claim> ParseJWTToken(string token);

        IEnumerable<Claim> ParseJWTPayload(string token);
    }
}
