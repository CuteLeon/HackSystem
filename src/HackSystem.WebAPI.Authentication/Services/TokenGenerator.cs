using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HackSystem.WebAPI.Authentication.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace HackSystem.WebAPI.Authentication.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly IOptionsSnapshot<JwtAuthenticationOptions> options;
    private readonly SymmetricSecurityKey securityKey;
    private readonly SigningCredentials signingCredentials;
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

    public TokenGenerator(
        IOptionsSnapshot<JwtAuthenticationOptions> options)
    {
        this.options = options;
        this.securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.Value.JwtSecurityKey));
        this.signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    public string GenerateSecurityToken(IEnumerable<Claim> claims)
    {
        // var expiry = new DateTime(2038, 01, 19, 03, 14, 07);
        var expiry = DateTime.Now.AddMinutes(this.options.Value.JwtExpiryInMinutes);

        var token = new JwtSecurityToken(
            this.options.Value.JwtIssuer,
            this.options.Value.JwtAudience,
            claims,
            expires: expiry,
            signingCredentials: this.signingCredentials);

        return this.jwtSecurityTokenHandler.WriteToken(token);
    }
}
