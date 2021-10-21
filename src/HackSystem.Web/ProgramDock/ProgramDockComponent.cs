using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
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
        this.StateHasChanged();
    }

    private void OnWindowChangeEvent(object sender, WindowChangeEvent e)
    {
        this.StateHasChanged();
    }

    public void ClearProgramDock()
    {
        this.UserProgramMaps = default;
        this.StateHasChanged();
    }

    public void LoadProgramDock(IEnumerable<UserProgramMap> maps)
    {
        this.UserProgramMaps = maps.ToDictionary(map => map.Program.Id);
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

    private IEnumerable<UserProgramMap> GetDockedRuningUserProgramMaps()
        => this.UserProgramMaps.Values.Where(map => map.PinToDock).ToArray();

    private IEnumerable<UserProgramMap> GetUndockedRuningUserProgramMaps()
        => this.processContainer.GetProcesses()
            .OrderBy(process => process.LaunchTime)
            .DistinctBy(process => process.ProgramDetail)
            .Select(process => process.ProgramDetail)
            .Select(program => this.UserProgramMaps.TryGetValue(program.Id, out var map) && !map.PinToDock ? map : default)
            .Where(map => map != null)
            .ToArray();
}
