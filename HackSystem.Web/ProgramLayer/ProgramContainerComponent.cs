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
        this.processContainer.ProcessChanged += ProcessChangedHandler;
        await base.OnInitializedAsync();
    }

    private void ProcessChangedHandler(ProcessChangeStates states, ProcessDetail processDetail)
    {
        this.logger.LogInformation($"Process {processDetail.PID} {states.ToString()}...");
        this.StateHasChanged();
    }

    public void Dispose()
    {
        this.processContainer.ProcessChanged -= ProcessChangedHandler;
    }
}
