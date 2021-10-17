using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockComponent
{
    public void ClearProgramDock()
    {
        this.UserProgramMaps.Clear();
        this.StateHasChanged();
    }

    public void LoadProgramDock(IEnumerable<UserProgramMap> maps)
    {
        this.UserProgramMaps.Clear();

        foreach (var map in maps)
        {
            this.UserProgramMaps.Add(map.Program.Id, map);
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

    public async Task OnClickIcon(ProgramIconMouseEventArgs args)
    {
        var programDetail = args.UserProgramMap.Program;
        this.logger.LogInformation($"Click to luanch program: {programDetail.Name}");
        await this.intermediaryRequestSender.Send(new ProgramLaunchRequest(programDetail));
    }
}
