using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using HackSystem.WebAPI.Extensions.WebAPILogs.DataServices;
using HackSystem.WebAPI.Model.WebLog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace HackSystem.WebAPI.Extensions.WebAPILogs.Middleware
{
    public class WebAPILoggingMiddleware
    {
        private readonly ILogger<WebAPILoggingMiddleware> logger;
        private readonly IWebAPILogDataService webAPILogDataService;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;
        private readonly RequestDelegate next;

        public WebAPILoggingMiddleware(
            ILogger<WebAPILoggingMiddleware> logger,
            IServiceScopeFactory serviceScopeFactory,
            RequestDelegate next)
        {
            this.logger = logger;
            var serviceScope = serviceScopeFactory.CreateScope();
            this.webAPILogDataService = serviceScope.ServiceProvider.GetRequiredService<IWebAPILogDataService>();
            this.recyclableMemoryStreamManager = serviceScope.ServiceProvider.GetRequiredService<RecyclableMemoryStreamManager>();
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            var watcher = new Stopwatch();
            watcher.Start();

            var webAPILog = new WebAPILog
            {
                RequestURI = context.Request.Path.ToUriComponent(),
                QueryString = context.Request.QueryString.ToUriComponent(),
                Method = context.Request.Method,
                SourceHost = $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}",
                UserAgent = context.Request.Headers.TryGetValue("User-Agent", out var userAgent) ? userAgent : string.Empty,
                TraceIdentifier = context.TraceIdentifier,
                IsAuthenticated = context.User.Identity?.IsAuthenticated ?? false,
                IdentityName = context.User.Identity?.Name,
                StartDateTime = DateTime.Now,
            };
            if (context.Request.Body.CanRead)
            {
                context.Request.EnableBuffering();
                await using var requestStream = this.recyclableMemoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStream);
                using var requestStreamReader = new StreamReader(requestStream);
                requestStream.Seek(0, SeekOrigin.Begin);
                webAPILog.RequestBody = await requestStreamReader.ReadToEndAsync();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            await this.webAPILogDataService.AddAsync(webAPILog);

            try
            {
                var originalResponseStream = context.Response.Body;
                await using var responseBodyStream = this.recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBodyStream;

                await this.next(context);

                using var responseStreamReader = new StreamReader(responseBodyStream);
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await responseStreamReader.ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                webAPILog.ResponseBody = responseBody;
                webAPILog.StatusCode = context.Response.StatusCode;
                await responseBodyStream.CopyToAsync(originalResponseStream);

                await this.webAPILogDataService.UpdateAsync(webAPILog);
            }
            catch (Exception ex)
            {
                webAPILog.Exception = ex.ToString();

                throw;
            }
            finally
            {
                watcher.Stop();
                webAPILog.FinishDateTime = DateTime.Now;
                webAPILog.ElapsedTime = watcher.ElapsedMilliseconds;
                await this.webAPILogDataService.UpdateAsync(webAPILog);
                this.logger.LogInformation($"Web API of {webAPILog.IdentityName} from {webAPILog.SourceHost} in {webAPILog.ElapsedTime} ms: [{webAPILog.Method}]=>{webAPILog.RequestURI} [{webAPILog.StatusCode}]");
            }
        }
    }
}
