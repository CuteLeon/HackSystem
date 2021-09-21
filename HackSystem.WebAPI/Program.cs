using HackSystem.Cryptography;
using HackSystem.Web.Authentication.Extensions;
using HackSystem.WebAPI.Authentication.Configurations;
using HackSystem.WebAPI.Configurations;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.Domain.Identity;
using HackSystem.WebAPI.Extensions;
using HackSystem.WebAPI.MockServer.Domain.Configurations;
using HackSystem.WebAPI.MockServer.Extensions;
using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using HackSystem.WebAPI.Services.Extensions;
using HackSystem.WebAPI.TaskServer.Domain.Configuration;
using HackSystem.WebAPI.TaskServer.Extensions;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
var assemblyName = typeof(Program).Assembly.GetName();
var setupInformation = AppDomain.CurrentDomain.SetupInformation;

try
{
    logger.Info($"{assemblyName.Name} launches, TargetFrameworkName={setupInformation.TargetFrameworkName}, Version={assemblyName.Version}");

    var builder = WebApplication.CreateBuilder(args);
    var env = builder.Environment;
    var config = builder.Configuration;
    var jwtConfiguration = config.GetSection("JwtConfiguration").Get<JwtAuthenticationOptions>();
    var taskServerConfiguration = config.GetSection("TaskServerConfiguration").Get<TaskServerOptions>();
    var mockServerConfiguration = config.GetSection("MockServerConfiguration").Get<MockServerOptions>();
    var securityConfiguration = config.GetSection("SecurityConfiguration").Get<SecurityConfiguration>();
    var programAssetConfiguration = config.GetSection("ProgramAssetConfiguration").Get<ProgramAssetOptions>();

    builder.Configuration
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

    builder.Logging
        .ClearProviders()
        .AddConsole()
        .AddDebug()
        .SetMinimumLevel(LogLevel.Trace)
        .AddNLogWeb();

    builder.Services
        .AddCors(options => options.AddPolicy("AllowAny", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()))
        .AddDbContext<HackSystemDBContext>(
            options => options
                .UseSqlite(config.GetConnectionString("HSDB"))
                .UseLazyLoadingProxies(),
            ServiceLifetime.Scoped)
        .AddIdentity<HackSystemUser, HackSystemRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 4;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Lockout.AllowedForNewUsers = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<HackSystemDBContext>();

    builder.Services
        .AddAutoMapper(typeof(Program).Assembly)
        .AddRSACryptography(options =>
        {
            options.RSAKeyParameters = securityConfiguration.RSAPrivateKey;
        })
        .AttachTaskServer(taskServerConfiguration)
        .AttachMockServer(mockServerConfiguration)
        .AddHttpClient()
        .AddMemoryCache()
        .AddHackSystemWebAPIExtensions()
        .AddAPIAuthentication(jwtConfiguration)
        .AddWebAPIServices()
        .AddProgramAssetServices(options =>
        {
            options.FolderPath = Path.IsPathFullyQualified(programAssetConfiguration.FolderPath) ?
                programAssetConfiguration.FolderPath :
                Path.GetFullPath(programAssetConfiguration.FolderPath, AppContext.BaseDirectory);
        });

    builder.Services
        .AddResponseCompression()
        .AddControllersWithViews()
        .AddNewtonsoftJson();

    var app = builder.Build();
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/HackSystemError");
        app.UseHsts();
    }

    app.UseCors("AllowAny")
        .UseHttpsRedirection()
        .UseStaticFiles()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseWebAPILogging()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        })
        .UseMockServer();

    app.InitializeDatabase()
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
