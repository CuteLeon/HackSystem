using System.Drawing;
using HackSystem.Web.ProgramPlatform.Components;
using HackSystem.Web.ProgramPlatform.Contracts;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace HackSystem.Web.ProgramPlatform.Windows.ProgramWindow;

public partial class DynamicProgramWindow : IDraggableComponent, IResizeableComponent, IAsyncDisposable
{
    private DotNetObjectReference<IDraggableComponent> draggableReference;
    private DotNetObjectReference<IResizeableComponent> resizeableReference;
    private Point dragStartPoint;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.draggableReference = DotNetObjectReference.Create<IDraggableComponent>(this);
        this.resizeableReference = DotNetObjectReference.Create<IResizeableComponent>(this);
        this.ProgramWindowDetail.Caption = this.ProgramWindowDetail.ProcessDetail.ProgramDetail.Name;
    }

    [JSInvokable]
    public async void UpdatePosition(double left, double top)
    {
        if (this.ProgramWindowDetail.WindowState == ProgramWindowStates.Maximized) return;

        this.Logger.LogInformation($"Sync window position at ({left}, {top}) {this.ProgramWindowDetail.WindowState}.");
        this.ProgramWindowDetail.Left = $"{left}px";
        this.ProgramWindowDetail.Top = $"{top}px";
    }

    [JSInvokable]
    public async void UpdateSize(double left, double top, double width, double height)
    {
        if (this.ProgramWindowDetail.WindowState == ProgramWindowStates.Maximized) return;

        this.Logger.LogInformation($"Sync window position at ({left}, {top}) and size as ({width}, {height}) {this.ProgramWindowDetail.WindowState}.");
        this.ProgramWindowDetail.Left = $"{left}px";
        this.ProgramWindowDetail.Top = $"{top}px";
        this.ProgramWindowDetail.Width = $"{width}px";
        this.ProgramWindowDetail.Height = $"{height}px";
    }

    public virtual async Task OnMin()
    {
        this.Logger.LogInformation($"Min window {this.ProgramWindowDetail.WindowId} of process {this.ProgramWindowDetail.ProcessDetail.ProcessId}.");
        this.ProgramWindowDetail.WindowState = ProgramWindowStates.Minimized;
        _ = await this.publisher.SendRequest(new WindowScheduleRequest(this.ProgramWindowDetail, WindowChangeStates.Inactive));
        this.StateHasChanged();
    }

    public virtual async Task OnMaxRestore()
    {
        if (this.ProgramWindowDetail.WindowState == ProgramWindowStates.Maximized)
            await this.OnRestore();
        else
            await this.OnMax();
    }

    public virtual async Task OnMax()
    {
        this.Logger.LogInformation($"Toggle max window {this.ProgramWindowDetail.WindowId} of process {this.ProgramWindowDetail.ProcessDetail.ProcessId}.");
        this.ProgramWindowDetail.WindowState = ProgramWindowStates.Maximized;
        _ = await this.publisher.SendRequest(new WindowScheduleRequest(this.ProgramWindowDetail, WindowChangeStates.Active));
        this.StateHasChanged();
    }

    public virtual async Task OnRestore()
    {
        this.Logger.LogInformation($"Toggle restore window {this.ProgramWindowDetail.WindowId} of process {this.ProgramWindowDetail.ProcessDetail.ProcessId}.");
        this.ProgramWindowDetail.WindowState = ProgramWindowStates.Normal;
        _ = await this.publisher.SendRequest(new WindowScheduleRequest(this.ProgramWindowDetail, WindowChangeStates.Active));
        this.StateHasChanged();
    }

    public virtual async Task OnClose()
    {
        this.Logger.LogInformation($"Close window {this.ProgramWindowDetail.WindowId} of process {this.ProgramWindowDetail.ProcessDetail.ProcessId}.");
        if (this.ProgramWindowDetail.IsModal)
            this.ProgramWindowDetail.SetModalWindowResult(ModalWindowResults.Cancel);
        await this.publisher.SendCommand(new WindowDestroyCommand(this.ProgramWindowDetail));
    }

    public async Task OnWindowFocusIn()
    {
        var windowScheduleResponse = await this.publisher.SendRequest(new WindowScheduleRequest(this.ProgramWindowDetail, WindowChangeStates.Active));
        if (windowScheduleResponse.Scheduled)
        {
            this.Logger.LogInformation($"Scheduled window {this.ProgramWindowDetail.WindowId} of process {this.ProgramWindowDetail.ProcessDetail.ProcessId}.");
        }
    }

    public async Task OnDoubleClickHeader()
    {
        if (!this.ProgramWindowDetail.AllowMaximized) return;
        await this.OnMaxRestore();
    }

    public async Task OnDragStart(Point position)
    {
        this.dragStartPoint = position;
    }

    public async Task OnDragEnd(Point position)
    {
        // MAGIC, No matter what, it works, at least...
        var distance = Math.Pow(position.X - this.dragStartPoint.X, 2) + Math.Pow(position.Y - this.dragStartPoint.Y, 2);
        if (distance < 50) return;

        if (this.ProgramWindowDetail.WindowState == ProgramWindowStates.Maximized)
        {
            // ANOTHER MAGIC, I resolved this issue in an I-DON'T-UNDERSTAND way.
            this.ProgramWindowDetail.Left = $"{position.X - dragStartPoint.X}px";
            this.ProgramWindowDetail.Top = $"{position.Y - dragStartPoint.Y + ComponentContract.TopBarHeight}px";
            await this.OnRestore();
        }
        else if (position.Y <= ComponentContract.TopBarHeight * 2 &&
            this.ProgramWindowDetail.AllowMaximized &&
            this.ProgramWindowDetail.WindowState == ProgramWindowStates.Normal)
        {
            // Tiny magic.
            await this.OnMax();
        }
    }

    public async Task OnToggleTopTier()
    {
        this.Logger.LogInformation($"Toggle top tier window {this.ProgramWindowDetail.WindowId} of process {this.ProgramWindowDetail.ProcessDetail.ProcessId}.");
        _ = await this.publisher.SendRequest(new WindowScheduleRequest(this.ProgramWindowDetail, WindowChangeStates.ToggleTopTier));
        this.StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
        this.draggableReference.Dispose();
        this.resizeableReference.Dispose();
    }
}
