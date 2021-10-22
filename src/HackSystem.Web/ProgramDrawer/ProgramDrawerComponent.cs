using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramDrawer;

public partial class ProgramDrawerComponent
{
    public void ClearProgramDrawer()
    {
        this.UserProgramMaps.Clear();
        this.StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await this.jsRuntime.InvokeVoidAsync("blazorJSTools.importJavaScript", "/js/hacksystem.programdrawer.js");
        }
    }

    public async Task LoadProgramDrawer(IEnumerable<UserProgramMap> maps)
    {
        foreach (var map in maps) this.UserProgramMaps.Add(map.Program.Id, map);
        this.StateHasChanged();
    }

    public async Task OnIconSelect(ProgramIconEventArgs args)
    {
        var programDetail = args.UserProgramMap.Program;
        this.logger.LogInformation($"Double click to launch program: {programDetail.Name}");
        await this.intermediaryRequestSender.Send(new ProgramLaunchRequest(programDetail));
    }
}
