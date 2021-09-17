using Microsoft.AspNetCore.HttpLogging;

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
