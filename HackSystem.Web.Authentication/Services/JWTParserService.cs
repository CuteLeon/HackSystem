using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Authentication.Services
{
    /// <summary>
    /// 从 JWT 解析 Claims
    /// </summary>
    public class JWTParserService : IJWTParserService
    {
        private readonly ILogger<JWTParserService> logger;

        public JWTParserService(ILogger<JWTParserService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Claim> ParseJWTToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException($"“{nameof(token)}”不能为 Null 或空白", nameof(token));
            }

            var parts = token.Split('.');

            return parts?.Length != 3 ?
                throw new FormatException($"“{nameof(token)}”不符合 JWT 格式") :
                this.ParseJWTPayload(parts[1]);
        }

        public IEnumerable<Claim> ParseJWTPayload(string payload)
        {
            this.logger.LogDebug($"解析 JWT Payload: {payload}");
            if (string.IsNullOrWhiteSpace(payload))
            {
                throw new ArgumentException($"“{nameof(payload)}”不能为 Null 或空白", nameof(payload));
            }

            var claims = new List<Claim>();
            var contentBytes = this.DecodeBase64(payload);
            var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(contentBytes);

            foreach (var pair in dictionary)
            {
                // 相同 Type 的多个 Claim 会被合并在一起，需要按照 JSON 数组的格式分拆为多个
                if (pair.Value is JsonElement jsonElement &&
                    jsonElement.ValueKind == JsonValueKind.Array)
                {
                    claims.AddRange(jsonElement.EnumerateArray().Select(element => new Claim(pair.Key, element.GetString())));
                }
                else
                {
                    claims.Add(new Claim(pair.Key, pair.Value.ToString()));
                }
            }

            this.logger.LogDebug($"解析 Claim: \n\t{string.Join("\n\t", claims.Select(claim => $"{claim.Type} = {claim.Value}"))}");
            return claims;
        }

        private byte[] DecodeBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                throw new ArgumentNullException(nameof(base64));
            }

            string text = base64;
            text = text.Replace('-', '+');
            text = text.Replace('_', '/');
            text = (text.Length % 4) switch
            {
                0 => text,
                int count when count is 2 or 3 => text.PadRight(text.Length + (4 - count), '='),
                _ => throw new FormatException("Invilad Base64 string."),
            };

            return Convert.FromBase64String(text);
        }
    }
}
