using System;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HackSystem.Web.Authentication.Services.Tests
{
    public class JWTParserServiceTests
    {
        [Theory()]
        [InlineData(null, null, typeof(ArgumentException))]
        [InlineData("", null, typeof(ArgumentException))]
        [InlineData("   ", null, typeof(ArgumentException))]
        [InlineData("!@#$%^&*()_+", null, typeof(FormatException))]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..lHgI8ZGPFMnqgcMD28RYCwtsAVT3PA5jH4gdd-CykVI", null, typeof(FormatException))]
        [InlineData(
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOlsidHJ1ZSIsInRydWUiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjE2MTM0NTM3OTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.lHgI8ZGPFMnqgcMD28RYCwtsAVT3PA5jH4gdd-CykVI",
            new[]
            {
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name=>Leon",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress=>leon@hack.com",
                "Professional=>true",
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role=>Hacker",
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role=>Commander",
                "exp=>1613453793",
                "iss=>https://localhost",
                "aud=>https://localhost"
            },
            null)]
        public void ParseJWTTokenTest(string token, string[] expectedClaims, Type expectedExceptionType)
        {
            var mockLogger = new Mock<ILogger<JWTParserService>>();
            IJWTParserService serviceInstance = new JWTParserService(mockLogger.Object);
            if (expectedExceptionType != null)
            {
                Assert.Throws(expectedExceptionType, delegate { serviceInstance.ParseJWTToken(token); });
            }
            else
            {
                var claims = serviceInstance.ParseJWTToken(token)
                    .Select(c => $"{c.Type}=>{c.Value}")
                    .ToHashSet();
                foreach (var claim in expectedClaims)
                {
                    Assert.True(claims.Contains(claim), $"Should contains expected claim => {claim}");
                }
            }
        }

        [Theory()]
        [InlineData(null, null, typeof(ArgumentException))]
        [InlineData("", null, typeof(ArgumentException))]
        [InlineData("   ", null, typeof(ArgumentException))]
        [InlineData("!@#$%^&*()_+", null, typeof(FormatException))]
        [InlineData("IUAjJCVeJiooKV8r", null, typeof(JsonException))]
        [InlineData("IUAjJCVeJiooKV8r=", null, typeof(FormatException))]
        [InlineData(
            "eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiOlsidHJ1ZSIsInRydWUiXSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAiOjE2MTM0NTM3OTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ",
            new[]
            {
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name=>Leon",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress=>leon@hack.com",
                "Professional=>true",
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role=>Hacker",
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role=>Commander",
                "exp=>1613453793",
                "iss=>https://localhost",
                "aud=>https://localhost"
            },
            null)]
        public void ParseJWTPayloadTest(string payload, string[] expectedClaims, Type expectedExceptionType)
        {
            var mockLogger = new Mock<ILogger<JWTParserService>>();
            IJWTParserService serviceInstance = new JWTParserService(mockLogger.Object);
            if (expectedExceptionType != null)
            {
                Assert.Throws(expectedExceptionType, delegate { serviceInstance.ParseJWTPayload(payload); });
            }
            else
            {
                var claims = serviceInstance.ParseJWTPayload(payload)
                    .Select(c => $"{c.Type}=>{c.Value}")
                    .ToHashSet();
                foreach (var claim in expectedClaims)
                {
                    Assert.True(claims.Contains(claim), $"Should contains expected claim => {claim}");
                }
            }
        }
    }
}