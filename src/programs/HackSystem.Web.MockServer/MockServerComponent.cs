using HackSystem.Web.Component.ToastContainer;
using HackSystem.Web.MockServer.Services;

namespace HackSystem.Web.MockServer;

public partial class MockServerComponent
{
    private IMockRouteService mockRouteService;
    private IServiceScope serviceScope;

    public MockServerComponent()
    {
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        this.serviceScope = this.ServiceScopeFactory.CreateScope();
        this.mockRouteService = new MockRouteService(
            this.serviceScope.ServiceProvider.GetRequiredService<ILogger<MockRouteService>>(),
            this.serviceScope.ServiceProvider.GetRequiredService<IHttpClientFactory>());
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await this.LoadMockRoutes();
        }
    }

    private async Task LoadMockRoutes()
    {
        this.Logger.LogInformation($"Loading Mock Routes...");
        await this.ClearMockRoutes();
        var mockRoutes = await this.mockRouteService.QueryMockRoutes();
        this.MockRoutes.AddRange(mockRoutes);
        this.Logger.LogInformation($"Loaded {mockRoutes.Count()} Mock Routes.");
        await this.ToastHandler.PopupToast(new ToastDetail
        {
            Title = "Load Successfully!",
            Icon = ToastIcons.Information,
            Message = $"Load {mockRoutes.Count()} Mock Routes successfully.",
        });
        this.StateHasChanged();
    }

    private async Task ClearMockRoutes()
    {
        this.MockRoutes.Clear();
        this.StateHasChanged();
    }
}
