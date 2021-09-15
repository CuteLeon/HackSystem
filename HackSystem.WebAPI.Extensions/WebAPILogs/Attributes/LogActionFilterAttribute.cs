using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace HackSystem.WebAPI.Extensions.WebAPILogs.Attributes;

public class LogActionFilterAttribute : ActionFilterAttribute
{
    public const string NoLogResponseBodyFlag = "NoLogResponseBody";
    public const string NoLogRequestBodyFlag = "NoLogRequestBody";

    private readonly StringValues TureHeaderValue = new(bool.TrueString);
    private readonly bool noLogRequestBody;
    private readonly bool noLogResponseBody;

    public LogActionFilterAttribute(bool noLogRequestBody = false, bool noLogResponseBody = false)
    {
        this.noLogRequestBody = noLogRequestBody;
        this.noLogResponseBody = noLogResponseBody;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (this.noLogRequestBody) context.HttpContext.Request.Headers.Add(NoLogRequestBodyFlag, TureHeaderValue);
        if (this.noLogResponseBody) context.HttpContext.Response.Headers.Add(NoLogResponseBodyFlag, TureHeaderValue);
        await base.OnActionExecutionAsync(context, next);
    }
}
