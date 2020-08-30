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
    public class JWTParser : IJWTParser
    {
        private readonly ILogger<JWTParser> logger;

        public JWTParser(ILogger<JWTParser> logger)
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
            this.logger.LogInformation($"解析 JWT Payload: {payload}");
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

            this.logger.LogInformation($"解析 Claim: \n\t{string.Join("\n\t", claims.Select(claim => $"{claim.Type} = {claim.Value}"))}");
            return claims;
        }

        public byte[] DecodeBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                throw new ArgumentException(nameof(base64));
            }

            string text = base64;
            text = text.Replace('-', '+');
            text = text.Replace('_', '/');
            switch (text.Length % 4)
            {
                case 2:
                    text += "==";
                    break;
                case 3:
                    text += "=";
                    break;
                default:
                    throw new FormatException("不合法的 Base64 字符串");
                case 0:
                    break;
            }

            return Convert.FromBase64String(text);
        }
    }
}
