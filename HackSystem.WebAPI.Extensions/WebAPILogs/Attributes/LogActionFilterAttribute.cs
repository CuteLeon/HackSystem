using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace HackSystem.WebAPI.Extensions.WebAPILogs.Attributes;

public class LogActionFilterAttribute : ActionFilterAttribute
{
    public const string LogResponseBodyFlag = "LogResponseBody";
    public const string LogRequestBodyFlag = "LogRequestBody";

    private readonly StringValues TureHeaderValue = new(bool.TrueString);
    private readonly bool logRequestBody;
    private readonly bool logResponseBody;

    public LogActionFilterAttribute(bool logRequestBody = true, bool logResponseBody = true)
    {
        this.logRequestBody = logRequestBody;
        this.logResponseBody = logResponseBody;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (this.logRequestBody) context.HttpContext.Response.Headers.Add(LogRequestBodyFlag, TureHeaderValue);
        if (this.logResponseBody) context.HttpContext.Response.Headers.Add(LogResponseBodyFlag, TureHeaderValue);
        await base.OnActionExecutionAsync(context, next);
    }
}
