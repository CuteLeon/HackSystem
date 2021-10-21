using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockComponent
{
    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.windowChangeEventHandler.EventRaised += OnWindowChangeEvent;
        this.processChangeEventHandler.EventRaised += OnProcessChangeEvent;
    }

    private void OnProcessChangeEvent(object sender, ProcessChangeEvent e)
    {
        if (e.ChangeStates == ProcessChangeStates.Launch)
        {
            if (this.DockedProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id) ||
                this.UndockedRunningProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id)) return;
            if (!this.UserProgramMaps.TryGetValue(e.ProcessDetail.ProgramDetail.Id, out var programMap)) return;

            (programMap.PinToDock ? DockedProgramMaps : UndockedRunningProgramMaps).Add(programMap.Program.Id, programMap);
            this.StateHasChanged();
        }
        else if (e.ChangeStates == ProcessChangeStates.Destroy)
        {
            if (this.DockedProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id) ||
                !this.UndockedRunningProgramMaps.ContainsKey(e.ProcessDetail.ProgramDetail.Id)) return;
            if (!this.UserProgramMaps.TryGetValue(e.ProcessDetail.ProgramDetail.Id, out var programMap)) return;
            if (e.ProcessDetail.ProgramDetail.GetProcessDetails().Any()) return;
            this.UndockedRunningProgramMaps.Remove(programMap.Program.Id);
            this.StateHasChanged();
        }
    }

    private void OnWindowChangeEvent(object sender, WindowChangeEvent e)
    {
        // TODO: LEON: Update ICON's windows collection of process
        this.StateHasChanged();
    }

    public void ClearProgramDock()
    {
        this.UserProgramMaps.Clear();
        this.DockedProgramMaps.Clear();
        this.UndockedRunningProgramMaps.Clear();
        this.StateHasChanged();
    }

    public void LoadProgramDock(IEnumerable<UserProgramMap> maps)
    {
        foreach (var map in maps)
        {
            this.UserProgramMaps.Add(map.Program.Id, map);
            if (map.PinToDock) this.DockedProgramMaps.Add(map.Program.Id, map);
            else if (map.Program.GetProcessDetails().Any()) this.UndockedRunningProgramMaps.Add(map.Program.Id, map);
        }
        this.StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await this.jsRuntime.InvokeVoidAsync("blazorJSTools.importJavaScript", "./js/hacksystem.programdock.js");
        }
    }

    public async Task OnIconSelect(ProgramIconEventArgs args)
    {
        var programDetail = args.UserProgramMap.Program;
        this.logger.LogInformation($"Click to luanch program: {programDetail.Name}");
        await this.intermediaryRequestSender.Send(new ProgramLaunchRequest(programDetail));
    }
}
