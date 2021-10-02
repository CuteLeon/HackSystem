using HackSystem.Web.ProgramSchedule.Domain.Notification;

namespace HackSystem.Web.ProgramLayer;

/// <summary>
/// Program container component
/// </summary>
/// <remarks>
/// Reference external Razor components from a .Net Core Razor Class Library
/// For more details https://github.com/dotnet/aspnetcore/issues/26228
/// </remarks>
public partial class ProgramContainerComponent : IProgramContainer
{
    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    public async Task Handle(ProgramLaunchNotification notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Program launched, program container component rendering...");
        this.StateHasChanged();
        await Task.CompletedTask;
    }

    public async Task Handle(ProcessDisposeNotification notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation($"Program closed, program container component rendering...");
        this.StateHasChanged();
        await Task.CompletedTask;
    }
}
