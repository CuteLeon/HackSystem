using System.Collections.Generic;
using System.Security.Claims;

namespace HackSystem.WebAPI.Authentication.Services
{
    public interface ITokenGenerator
    {
        string GenerateSecurityToken(IEnumerable<Claim> claims);
    }
}
