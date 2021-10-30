namespace HackSystem.Web.MockServer;

public partial class MockRouteComponent
{
    public MockRouteComponent()
    {
    }

    private async Task SwitchMockRouteEnable(bool enable)
    {
        this.Logger.LogInformation($"Switch Task {this.MockRoute.RouteName} ({this.MockRoute.RouteID}) Enable to {(enable ? "Enabled" : "Disabled")}...");
    }
}
