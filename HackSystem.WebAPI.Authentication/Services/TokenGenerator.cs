using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HackSystem.WebAPI.Authentication.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HackSystem.WebAPI.Authentication.Services;

    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtAuthenticationOptions configuration;
        private readonly SymmetricSecurityKey securityKey;
        private readonly SigningCredentials signingCredentials;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

        public TokenGenerator(
            IOptionsMonitor<JwtAuthenticationOptions> optionsMonitor)
        {
            this.configuration = optionsMonitor.CurrentValue;
            this.securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.JwtSecurityKey));
            this.signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string GenerateSecurityToken(IEnumerable<Claim> claims)
        {
            var expiry = DateTime.Now.AddMinutes(this.configuration.JwtExpiryInMinutes);

            var token = new JwtSecurityToken(
                this.configuration.JwtIssuer,
                this.configuration.JwtAudience,
                claims,
                expires: expiry,
                signingCredentials: this.signingCredentials);

            return this.jwtSecurityTokenHandler.WriteToken(token);
        }
    }
