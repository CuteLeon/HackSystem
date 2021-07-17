using System;
using System.IO;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.SeedData;
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
                    //.InitializeIdentityData()
                    //.InitializeUserProgramMapData()
                    .LaunchTaskServer()
                    .Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{assemblyName.Name} launches failed.");
            }
            finally
            {
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(options =>
                {
                    options.SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("hosting.json", optional: true);
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
