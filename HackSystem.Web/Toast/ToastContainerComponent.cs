using HackSystem.Web.Toast.Handler;
using HackSystem.Web.Toast.Model;
using static HackSystem.Web.Toast.Model.ToastDetail;

namespace HackSystem.Web.Toast;

/// <summary>
/// Toast container component
/// </summary>
/// <remarks> Should works with blazor.toast.js </remarks>
public partial class ToastContainerComponent : IToastContainer, IDisposable
{
    private DotNetObjectReference<IToastContainer> interopReference;
    private IJSObjectReference toastJSObjectReference;
    private bool disposedValue;

    public ToastContainerComponent()
    {
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        this.interopReference = DotNetObjectReference.Create<IToastContainer>(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            this.toastJSObjectReference = await this.jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/hacksystem.toast.js");
        }
    }

    public void PopToast(string title, string message, Icons icon = Icons.HackSystem, bool autoHide = true, int hideDelay = 3000)
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
    }

    [JSInvokable]
    public void CloseToast(string toastId)
    {
        this.Toasts.Remove(toastId);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.Toasts.Clear();
                this.interopReference.Dispose();
                this.toastJSObjectReference.DisposeAsync().AsTask().ConfigureAwait(false);
            }

            this.disposedValue = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
