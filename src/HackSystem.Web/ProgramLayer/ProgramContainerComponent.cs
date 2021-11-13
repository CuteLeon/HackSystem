using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramLayer;

/// <summary>
/// Program container component
/// </summary>
/// <remarks>
/// Reference external Razor components from a .Net Core Razor Class Library
/// For more details https://github.com/dotnet/aspnetcore/issues/26228
/// </remarks>
public partial class ProgramContainerComponent : IDisposable
{
    protected async override Task OnInitializedAsync()
    {
        this.windowChangeEventHandler.EventRaised += this.OnWindowChangeEvent;
        await base.OnInitializedAsync();
    }

    private void OnWindowChangeEvent(object sender, WindowChangeEvent args)
    {
        this.logger.LogInformation($"Render program layer when window schedule: {args.WindowDetail.Caption}...");
        this.StateHasChanged();
    }

    public void Dispose()
    {
        this.windowChangeEventHandler.EventRaised -= this.OnWindowChangeEvent;
    }
}
