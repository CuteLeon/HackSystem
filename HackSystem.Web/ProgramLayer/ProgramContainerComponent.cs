using HackSystem.Web.ProgramSchedule.Domain.Notification;

namespace HackSystem.Web.ProgramLayer;

/// <summary>
/// Program container component
/// </summary>
/// <remarks>
/// Reference external Razor components from a .Net Core Razor Class Library
/// For more details https://github.com/dotnet/aspnetcore/issues/26228
/// </remarks>
public partial class ProgramContainerComponent
{
    private bool disposedValue;

    protected async override Task OnInitializedAsync()
    {
        // TODO: LEON: Render after launch or close process;
        //this.programLaunchSubcriber.HandleMessage = this.HandleProgramLaunchMessage;
        //this.processCloseSubscriber.HandleMessage = this.HandleProcessCloseMessage;

        await base.OnInitializedAsync();
    }

    private async Task HandleProgramLaunchMessage(ProgramLaunchNotification notification)
    {
        this.logger.LogInformation($"Program launched, program container component rendering...");
        this.StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task HandleProcessCloseMessage(ProcessCloseNotification notification)
    {
        this.logger.LogInformation($"Program closed, program container component rendering...");
        this.StateHasChanged();
        await Task.CompletedTask;
    }
}
