using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;

namespace HackSystem.WebHost;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddLogging()
            .AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.All;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }

        app.UseRouting();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
