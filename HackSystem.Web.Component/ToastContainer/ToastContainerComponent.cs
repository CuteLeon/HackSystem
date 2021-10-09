using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace HackSystem.Web.Component.ToastContainer;

/// <summary>
/// Toast container component
/// </summary>
/// <remarks> Should works with blazor.toast.js </remarks>
public partial class ToastContainerComponent : IAsyncDisposable
{
    private DotNetObjectReference<ToastContainerComponent> interopReference;
    private Lazy<Task<IJSObjectReference>> moduleTask;

    public ToastContainerComponent()
    {
    }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.interopReference = DotNetObjectReference.Create(this);
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
           "import", "./_content/HackSystem.Web.Component/hacksystem.toast.js").AsTask());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task PopToast(string title, string message, ToastIcons icon = ToastIcons.HackSystem, bool autoHide = true, int hideDelay = 3000)
    {
        this.logger.LogDebug($"Pop a Toast: {title}");
        var toast = new ToastDetail()
        {
            Title = title,
            Message = message,
            Icon = icon,
            AutoHide = autoHide,
            HideDelay = hideDelay,
        };

        this.Toasts.Add(toast.Id, toast);
        this.StateHasChanged();

        // MAGIC! DO NOT TOUCH! This component is losing the status which from Bootstrap's initial method after each time renderred, have to re-initial after that.
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("toasts.popToast", this.interopReference, toast.Id, toast.AutoHide, toast.HideDelay);
    }

    [JSInvokable]
    public void CloseToast(string toastId)
    {
        this.Toasts.Remove(toastId);
    }

    public async ValueTask DisposeAsync()
    {
        this.Toasts.Clear();
        this.interopReference.Dispose();
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
