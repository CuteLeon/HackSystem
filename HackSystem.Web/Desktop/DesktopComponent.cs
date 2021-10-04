using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.ProgramSchedule.Domain.Entity;
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
            this.logger.LogInformation("Query programs ...");
            var maps = await this.programDetailService.QueryUserProgramMaps();
            if (maps?.Any() ?? false)
            {
                var mapDetails = this.mapper.Map<IEnumerable<UserProgramMapResponse>, IEnumerable<UserProgramMap>>(maps);
                this.UserProgramMaps = mapDetails.ToDictionary(map => map.Program.Id, map => map);
                this.ProgramDrawerComponent.LoadProgramDrawer(mapDetails);
                this.ProgramDockComponent.LoadProgramDock(mapDetails.Where(map => map.PinToDock));
            }
            else
            {
                this.ProgramDockComponent.ClearProgramDock();
                this.ProgramDrawerComponent.ClearProgramDrawer();
                this.UserProgramMaps.Clear();
            }
            this.logger.LogInformation($"Query {maps.Count()} programs successfully.");
            this.GetDesktopToastContainer().PopToast("Query programs successfully", $"Query programs successfully, total of {this.UserProgramMaps.Count}.", Icons.Information, true, 3000);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, $"Query programs failed.");
            this.GetDesktopToastContainer().PopToast("Failed to query programs", $"Failed to query programs: {ex.Message}", Icons.Error, false);
        }
    }
}
