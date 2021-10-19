using System.Security.Claims;
using System.Text.Json;

namespace HackSystem.Web.Authentication.TokenHandlers;

/// <summary>
/// Parse claims from JWT
/// </summary>
public class JsonWebTokenParser : IJsonWebTokenParser
{
    private readonly ILogger<JsonWebTokenParser> logger;

    public JsonWebTokenParser(ILogger<JsonWebTokenParser> logger)
    {
        this.logger = logger;
    }

    public IEnumerable<Claim> ParseJWTToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentNullException(nameof(token));
        }

        var parts = token.Split('.');

        return parts?.Length != 3 ?
            throw new FormatException($"Invalid {nameof(token)}") :
            this.ParseJWTPayload(parts[1]);
    }

    public IEnumerable<Claim> ParseJWTPayload(string payload)
    {
        this.logger.LogDebug($"Parse JWT Payload: {payload}");
        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new ArgumentNullException(nameof(payload));
        }

        var claims = new List<Claim>();
        var contentBytes = DecodeBase64(payload);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(contentBytes);

        foreach (var pair in dictionary)
        {
            // Claims with the same type are combined, should split via JSON format.
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

        this.logger.LogDebug($"Parse Claim: \n\t{string.Join("\n\t", claims.Select(claim => $"{claim.Type} = {claim.Value}"))}");
        return claims;
    }

    private static byte[] DecodeBase64(string base64)
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
