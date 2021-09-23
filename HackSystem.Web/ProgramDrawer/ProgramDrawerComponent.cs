using HackSystem.Web.ProgramDrawer.ProgramDrawerEventArgs;
using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.Web.ProgramDrawer;

public partial class ProgramDrawerComponent
{
    public void ClearProgramDrawer()
    {
        this.BasicProgramMaps.Clear();
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

    public void LoadProgramDrawer(IEnumerable<UserBasicProgramMapResponse> maps)
    {
        this.BasicProgramMaps.Clear();

        foreach (var map in maps.OrderByDescending(map => map.PinToTop))
        {
            this.BasicProgramMaps.Add(map.BasicProgram.Id, map);
        }
        this.StateHasChanged();
    }

    public async Task OnDoubleClickIcon(ProgramDrawerIconMouseEventArgs args)
    {
        this.logger.LogInformation($"Double click to luanch program: {args.UserBasicProgramMap.BasicProgram.Name}");
        await this.programLauncher.LaunchProgram(args.UserBasicProgramMap.BasicProgram);
    }
}
