using HackSystem.Web.ProgramPlatform.Components;
using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace HackSystem.Web.ProgramPlatform.Windows.ProgramWindow;

public partial class DynamicProgramWindow : IDraggableComponent, IResizeableComponent, IAsyncDisposable
{
    private DotNetObjectReference<IDraggableComponent> draggableReference;
    private DotNetObjectReference<IResizeableComponent> resizeableReference;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.draggableReference = DotNetObjectReference.Create<IDraggableComponent>(this);
        this.resizeableReference = DotNetObjectReference.Create<IResizeableComponent>(this);
    }

    [JSInvokable]
    public void UpdatePosition(double left, double top)
    {
        this.ProgramWindowStyle.Left = $"{left}px";
        this.ProgramWindowStyle.Top = $"{top}px";
    }

    [JSInvokable]
    public void UpdateSize(double left, double top, double width, double height)
    {
        this.UpdatePosition(left, top);
        this.ProgramWindowStyle.Width = $"{width}px";
        this.ProgramWindowStyle.Height = $"{height}px";
    }

    public virtual void OnMin()
    {
        this.Logger.LogInformation($"Min window {this.ProgramWindowDetail.WindowId} of process {this.ProcessDetail.ProcessId}.");
        this.ProgramWindowStyle.WindowState = ProgramWindowStates.Minimized;
        this.StateHasChanged();
    }

    public virtual void OnMaxRestore()
    {
        this.Logger.LogInformation($"Switch max window {this.ProgramWindowDetail.WindowId} of process {this.ProcessDetail.ProcessId}.");
        this.ProgramWindowStyle.WindowState = this.ProgramWindowStyle.WindowState == ProgramWindowStates.Maximized ?
            ProgramWindowStates.Normal :
            ProgramWindowStates.Maximized;
        this.StateHasChanged();
    }

    public virtual void OnClose()
    {
        this.Logger.LogInformation($"Close window {this.ProgramWindowDetail.WindowId} of process {this.ProcessDetail.ProcessId}.");
        this.CommandSender.Send(new WindowDestroyCommand(this.ProgramWindowDetail));
    }

    protected async Task OnWindowFocusIn()
    {
        var windowScheduleResponse = await this.RequestSender.Send(new WindowScheduleRequest(this.ProgramWindowDetail, WindowScheduleStates.Schedule));
        if (windowScheduleResponse.Scheduled)
        {
            this.Logger.LogInformation($"Scheduled window {this.ProgramWindowDetail.WindowId} of process {this.ProcessDetail.ProcessId}.");
        }
    }

    public async ValueTask DisposeAsync()
    {
        this.draggableReference.Dispose();
        this.resizeableReference.Dispose();
    }
}
