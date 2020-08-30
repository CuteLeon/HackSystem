namespace HackSystem.WebAPI.Configurations
{
    public class JwtConfiguration
    {
        public string JwtSecurityKey { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtAudience { get; set; }

        public int JwtExpiryInMinutes { get; set; }
    }
}
