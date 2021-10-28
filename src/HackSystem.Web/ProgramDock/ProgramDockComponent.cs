using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.Domain.Intermediary;
using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockComponent : IAsyncDisposable
{
    private DotNetObjectReference<ProgramDockComponent> programDockReference;

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.windowChangeEventHandler.EventRaised += OnWindowChangeEvent;
        this.processChangeEventHandler.EventRaised += OnProcessChangeEvent;
        this.programMapEventHandler.EventRaised += OnProgramMapEventHandle;
        this.programDockReference = DotNetObjectReference.Create<ProgramDockComponent>(this);
        await this.jsRuntime.InvokeVoidAsync("blazorJSTools.importJavaScript", "./js/hacksystem.programdock.js");
    }

    private async void OnProgramMapEventHandle(object sender, UserProgramMapEvent args)
    {
        var request = args.UserProgramMap;
        if (!this.UserProgramMaps.TryGetValue(request.ProgramId, out var programMap)) return;

        if (request.PinToDock.HasValue)
        {
            programMap.PinToDock = request.PinToDock.Value;
            if (programMap.PinToDock)
            {
                if (this.UndockedRunningProgramMaps.ContainsKey(programMap.Program.Id))
                {
                    this.UndockedRunningProgramMaps.Remove(programMap.Program.Id);
                }
                this.DockedProgramMaps[programMap.Program.Id] = programMap;
            }
            else if (!programMap.PinToDock)
            {
                if (this.DockedProgramMaps.ContainsKey(programMap.Program.Id))
                {
                    this.DockedProgramMaps.Remove(programMap.Program.Id);
                }
                if (programMap.Program.GetProcessDetails().Any())
                {
                    this.UndockedRunningProgramMaps[programMap.Program.Id] = programMap;
                }
            }
        }

        this.StateHasChanged();
    }

    private async void OnProcessChangeEvent(object sender, ProcessChangeEvent e)
    {
        if (e.ChangeState == ProcessChangeStates.Launch)
        {
            if (this.DockedProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id) ||
                this.UndockedRunningProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id)) return;
            if (!this.UserProgramMaps.TryGetValue(e.ProcessDetail.ProgramDetail.Id, out var programMap)) return;

            this.logger.LogInformation($"Launch process {e.ProcessDetail.ProcessId}, add to program dock...");
            (programMap.PinToDock ? DockedProgramMaps : UndockedRunningProgramMaps).Add(programMap.Program.Id, programMap);
            this.StateHasChanged();
        }
        else if (e.ChangeState == ProcessChangeStates.Destroy)
        {
            if (this.DockedProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id) ||
                !this.UndockedRunningProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id)) return;
            if (!this.UserProgramMaps.TryGetValue(e.ProcessDetail.ProgramDetail.Id, out var programMap)) return;
            if (e.ProcessDetail.ProgramDetail.GetProcessDetails().Any()) return;

            this.logger.LogInformation($"Destory process {e.ProcessDetail.ProcessId}, remove from program dock...");
            this.UndockedRunningProgramMaps.Remove(programMap.Program.Id);
            this.StateHasChanged();
        }
    }

    private async void OnWindowChangeEvent(object sender, WindowChangeEvent e)
    {
        var programId = e.WindowDetail?.ProcessDetail?.ProgramDetail?.Id;
        if (programId is null) return;
        if (!this.DockIconComponents.TryGetValue(programId, out var dockIconComponent)) return;

        this.logger.LogInformation($"Window {e.WindowDetail.Caption} {e.ChangeState}, update Dock Icon Component...");
        await dockIconComponent.UpdateWindowDetail(e.WindowDetail, e.ChangeState);
    }

    public void ClearProgramDock()
    {
        this.UserProgramMaps.Clear();
        this.DockedProgramMaps.Clear();
        this.UndockedRunningProgramMaps.Clear();
        this.StateHasChanged();
    }

    public async Task LoadProgramDock(IEnumerable<UserProgramMap> maps)
    {
        foreach (var map in maps)
        {
            this.UserProgramMaps.Add(map.Program.Id, map);
            if (map.PinToDock) this.DockedProgramMaps.Add(map.Program.Id, map);
            else if (map.Program.GetProcessDetails().Any()) this.UndockedRunningProgramMaps.Add(map.Program.Id, map);
        }
        this.StateHasChanged();

        await this.jsRuntime.InvokeVoidAsync("programDock.initialProgramDock", this.programDockReference);
    }

    public async Task OnIconSelect(ProgramIconEventArgs args)
    {
        var programDetail = args.UserProgramMap.Program;
        this.logger.LogInformation($"Click to launch program: {programDetail.Name}");
        await this.publisher.SendRequest(new ProgramLaunchRequest(programDetail));
    }

    [JSInvokable]
    public async Task OnWindowClick(string programId, int processId, string windowId)
    {
        if (this.UserProgramMaps.TryGetValue(programId, out var programMap) &&
            programMap.Program.TryGetProcessDetail(processId, out var process) &&
            process.TryGetWindowDetail(windowId, out var window))
        {
            await this.publisher.SendRequest(new WindowScheduleRequest(window, WindowChangeStates.ToggleActive));
        }
    }

    [JSInvokable]
    public async Task OnWindowStickyTop(string programId, int processId, string windowId)
    {
        if (this.UserProgramMaps.TryGetValue(programId, out var programMap) &&
            programMap.Program.TryGetProcessDetail(processId, out var process) &&
            process.TryGetWindowDetail(windowId, out var window))
        {
            await this.publisher.SendRequest(new WindowScheduleRequest(window, WindowChangeStates.ToggleTopTier));
        }
    }

    [JSInvokable]
    public async Task OnWindowClose(string programId, int processId, string windowId)
    {
        if (this.UserProgramMaps.TryGetValue(programId, out var programMap) &&
            programMap.Program.TryGetProcessDetail(processId, out var process) &&
            process.TryGetWindowDetail(windowId, out var window))
        {
            await this.publisher.SendCommand(new WindowDestroyCommand(window));
        }
    }

    [JSInvokable]
    public async Task OnRunClick(string programId)
    {
        if (this.UserProgramMaps.TryGetValue(programId, out var programMap))
        {
            await this.publisher.SendRequest(new ProgramLaunchRequest(programMap.Program));
        }
    }

    [JSInvokable]
    public async Task OnPinToDockClick(string programId)
    {
        if (this.UserProgramMaps.TryGetValue(programId, out var programMap))
        {
            var programMapCommand = new UserProgramMapCommand(new UserProgramMapRequest()
            {
                ProgramId = programMap.Program.Id,
                PinToDock = !programMap.PinToDock,
            });
            await this.publisher.SendCommand(programMapCommand);
        }
    }

    public async ValueTask DisposeAsync()
    {
        this.windowChangeEventHandler.EventRaised -= OnWindowChangeEvent;
        this.processChangeEventHandler.EventRaised -= OnProcessChangeEvent;
        this.programMapEventHandler.EventRaised -= OnProgramMapEventHandle;
    }
}
