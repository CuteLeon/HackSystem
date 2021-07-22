using System;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.TaskServers.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace HackSystem.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            var assemblyName = typeof(Program).Assembly.GetName();
            var setupInformation = AppDomain.CurrentDomain.SetupInformation;

            try
            {
                logger.Info($"{assemblyName.Name} launches, TargetFrameworkName={setupInformation.TargetFrameworkName}, Version={assemblyName.Version}");

                CreateHostBuilder(args)
                    .Build()
                    .InitializeDatabase()
                    .LaunchTaskServer()
                    .Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{assemblyName.Name} launches failed.");
            }
            finally
            {
                // HackSystemTaskServerExtension.ShutdownTaskServer();
                logger.Info($"{assemblyName.Name} shutdown, Version={assemblyName.Version}");
                NLog.LogManager.Shutdown();
            }
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
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureLogging(builder =>
                        {
                            builder.ClearProviders();
                            builder.AddConsole();
                            builder.AddDebug();
                            builder.SetMinimumLevel(LogLevel.Trace);
                        })
                        .UseNLog();
                });
    }
}
