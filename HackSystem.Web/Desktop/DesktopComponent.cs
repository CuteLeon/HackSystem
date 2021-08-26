using Microsoft.Extensions.Logging;
using static HackSystem.Web.Toast.Model.ToastDetail;

namespace HackSystem.Web.Desktop;

public partial class DesktopComponent
{
    public DesktopComponent()
    {
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    private void OnTest()
    {
        this.logger.LogInformation("On Test ...");
        this.GetDesktopToastContainer().PopToast("Hack System", "Will be closed automatically in 1 second.", Icons.Information, true, 1000);
        this.GetDesktopToastContainer().PopToast("Hack System", "Will be closed automatically in 2 second.", Icons.Question, true, 2000);
        this.GetDesktopToastContainer().PopToast("Hack System", "Will be closed automatically in 3 second.", Icons.Warning, true, 3000);
        this.GetDesktopToastContainer().PopToast("Hack System", "Should be closed manually.", Icons.Error, false);
        this.GetDesktopToastContainer().PopToast("Hack System", "Welcome to access Hack System.", Icons.HackSystem, false);
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await this.LoadProgramIcons();
            this.StateHasChanged();
        }
    }

    private async Task LoadProgramIcons()
    {
        try
        {
            var maps = await this.basicProgramService.QueryUserBasicProgramMaps();
            if (maps?.Any() ?? false)
            {
                this.BasicProgramMaps = maps.ToDictionary(map => map.BasicProgram.Id, map => map);
                this.ProgramDrawerComponent.LoadProgramDrawer(maps);
                this.ProgramDockComponent.LoadProgramDock(maps.Where(map => map.PinToDock));
            }
            else
            {
                this.ProgramDockComponent.ClearProgramDock();
                this.ProgramDrawerComponent.ClearProgramDrawer();
                this.BasicProgramMaps.Clear();
            }
            this.GetDesktopToastContainer().PopToast("Query programs successfully", $"Query programs successfully, total of {this.BasicProgramMaps.Count}.", Icons.Information, true, 3000);
        }
        catch (Exception ex)
        {
            this.GetDesktopToastContainer().PopToast("Failed to query programs", $"Failed to query programs: {ex.Message}", Icons.Error, false);
        }
    }
}
