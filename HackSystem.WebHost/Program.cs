namespace HackSystem.WebHost;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostBuilderContext, configuration) =>
            {
                var env = hostBuilderContext.HostingEnvironment;
                configuration.AddJsonFile("appsettings.json", true, true)
                             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
