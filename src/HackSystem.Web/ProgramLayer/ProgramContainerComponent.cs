using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;

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
        this.windowScheduleRequestHandler.OnWindowSchedule += this.WindowScheduleHandler;
        await base.OnInitializedAsync();
    }

    private void ProcessChangedHandler(ProcessChangeStates states, ProcessDetail processDetail)
    {
        this.logger.LogInformation($"Render program layer when process change: {processDetail.ProcessId} {states.ToString()}...");
        this.StateHasChanged();
    }

    private void WindowScheduleHandler(ProgramWindowDetail programWindowDetail)
    {
        this.logger.LogInformation($"Render program layer when window schedule: {programWindowDetail.Caption}...");
        this.StateHasChanged();
    }

    public void Dispose()
    {
        this.windowScheduleRequestHandler.OnWindowSchedule -= this.WindowScheduleHandler;
    }
}
