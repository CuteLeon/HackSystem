using HackSystem.WebDataTransfer.Program;
using Microsoft.JSInterop;

namespace HackSystem.Web.ProgramDock;

public partial class ProgramDockComponent
{
    public void ClearProgramDock()
    {
        this.BasicProgramMaps.Clear();
        this.StateHasChanged();
    }

    public void LoadProgramDock(IEnumerable<UserBasicProgramMapDTO> maps)
    {
        this.BasicProgramMaps.Clear();

        foreach (var map in maps)
        {
            this.BasicProgramMaps.Add(map.BasicProgram.Id, map);
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
