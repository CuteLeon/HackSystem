using Microsoft.Extensions.Configuration;

namespace HackSystem.Host.Configs
{
    /// <summary>
    /// Configuration of Host
    /// </summary>
    public static class HostConfigs
    {
        private const string ConfigFileName = "HostConfigs.json";

        public static readonly IConfigurationRoot Configuration;

        static HostConfigs()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(ConfigFileName);
            Configuration = configBuilder.Build();
        }

        public static string Title { get => Configuration["Title"]; }

        public static string RemoteURL { get => Configuration["RemoteURL"]; }

        public static string StartURI { get => Configuration["StartURI"]; }
    }
}
