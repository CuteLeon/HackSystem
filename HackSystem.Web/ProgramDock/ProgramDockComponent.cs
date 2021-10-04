using HackSystem.Web.ProgramSchedule.Domain.Entity;

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
}
