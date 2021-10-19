namespace HackSystem.Web.Component.ToastContainer;

/// <summary>
/// Toast container component
/// </summary>
/// <remarks> Should works with blazor.toast.js </remarks>
public partial class ToastContainerComponent : IAsyncDisposable
{
    private DotNetObjectReference<ToastContainerComponent> toastContainerReference;
    private IJSObjectReference toastModuleReference;

    public ToastContainerComponent()
    {
    }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.toastContainerReference = DotNetObjectReference.Create(this);
        this.toastEventHandler.EventRaised += this.HandlerToast;
        this.toastModuleReference = await jsRuntime
            .InvokeAsync<IJSObjectReference>("import", "./_content/HackSystem.Web.Component/hacksystem.toast.js");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
    }

    public async void HandlerToast(object? sender, ToastEvent toastEvent)
    {
        var toast = toastEvent.ToastDetail;
        this.logger.LogInformation($"Handle toast event: {toast.Id} ({toast.Title})...");
        this.Toasts.Add(toast.Id, toast);
        this.StateHasChanged();
    }

    [JSInvokable]
    public void CloseToast(string toastId)
    {
        this.Toasts.Remove(toastId);
        this.StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
        this.Toasts.Clear();
        this.toastContainerReference.Dispose();
        await this.toastModuleReference.DisposeAsync();
    }
}
