using HackSystem.Web.ProgramSDK.ProgramComponent.Messages;

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
    private bool disposedValue;

    protected async override Task OnInitializedAsync()
    {
        this.programLaunchSubcriber.HandleMessage = this.HandleProgramLaunchMessage;
        this.processCloseSubscriber.HandleMessage = this.HandleProcessCloseMessage;

        await base.OnInitializedAsync();
    }

    private async Task HandleProgramLaunchMessage(ProgramLaunchMessage message)
    {
        this.logger.LogInformation($"Program launched, program container component rendering...");
        this.StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task HandleProcessCloseMessage(ProcessCloseMessage arg)
    {
        this.logger.LogInformation($"Program closed, program container component rendering...");
        this.StateHasChanged();
        await Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.programLaunchSubcriber.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
