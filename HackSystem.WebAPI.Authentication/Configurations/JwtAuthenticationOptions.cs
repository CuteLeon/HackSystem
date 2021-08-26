namespace HackSystem.WebAPI.Authentication.Configurations;

    public class JwtAuthenticationOptions
    {
        public string JwtSecurityKey { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtAudience { get; set; }

        public int JwtExpiryInMinutes { get; set; }
    }
