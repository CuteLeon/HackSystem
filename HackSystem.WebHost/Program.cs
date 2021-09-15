using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Services
    .AddLogging()
    .AddHttpLogging(options =>
    {
        options.LoggingFields = HttpLoggingFields.All;
    });

builder.Configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

var app = builder.Build();
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage()
        .UseWebAssemblyDebugging();
}

app.UseRouting()
    .UseBlazorFrameworkFiles()
    .UseStaticFiles()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapFallbackToFile("index.html");
    });

app.Run();
