using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.ProgramSchedule.Domain.Entity;

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
        // TODO: Leon: Use Intermediary to popup toast
        // this.GetDesktopToastContainer().PopToast("Hack System", "Will be closed automatically in 1 second.", ToastIcons.Information, true, 1000);
        // this.GetDesktopToastContainer().PopToast("Hack System", "Will be closed automatically in 2 second.", ToastIcons.Question, true, 2000);
        // this.GetDesktopToastContainer().PopToast("Hack System", "Will be closed automatically in 3 second.", ToastIcons.Warning, true, 3000);
        // this.GetDesktopToastContainer().PopToast("Hack System", "Should be closed manually.", ToastIcons.Error, false);
        // this.GetDesktopToastContainer().PopToast("Hack System", "Welcome to access Hack System.", ToastIcons.HackSystem, false);
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
            // TODO: Leon: Use Intermediary to popup toast
            // this.GetDesktopToastContainer().PopToast("Query programs successfully", $"Query programs successfully, total of {this.UserProgramMaps.Count}.", ToastIcons.Information, true, 3000);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, $"Query programs failed.");
            // TODO: Leon: Use Intermediary to popup toast
            // this.GetDesktopToastContainer().PopToast("Failed to query programs", $"Failed to query programs: {ex.Message}", ToastIcons.Error, false);
        }
    }
}
