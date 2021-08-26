namespace HackSystem.Web.Configurations;

    public class APIConfiguration
    {
        public string APIHost { get; set; }

        public int TokenExpiryInMinutes { get; set; }

        public int TokenRefreshInMinutes { get; set; }
    }
