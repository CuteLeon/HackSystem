﻿using HackSystem.DataTransferObjects.Programs;
using HackSystem.Web.Component.ToastContainer;
using HackSystem.Web.ProgramSchedule.Entity;

namespace HackSystem.Web.Desktop;

public partial class DesktopComponent
{
    public DesktopComponent()
    {
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        this.authenticationTokenRefresher.StartRefresher();
    }

    private async void OnTest()
    {
        this.logger.LogInformation("On Test ...");
        await this.toastHandler.PopupToast(new ToastDetail
        {
            Title = "Hack System",
            AutoHide = true,
            HideDelay = 1000,
            Icon = ToastIcons.Information,
            Message = "Will be closed automatically in 1 second."
        });
        await this.toastHandler.PopupToast(new ToastDetail
        {
            Title = "Hack System",
            AutoHide = true,
            HideDelay = 2000,
            Icon = ToastIcons.Question,
            Message = "Will be closed automatically in 2 second."
        });
        await this.toastHandler.PopupToast(new ToastDetail
        {
            Title = "Hack System",
            AutoHide = true,
            HideDelay = 3000,
            Icon = ToastIcons.Warning,
            Message = "Will be closed automatically in 3 second."
        });
        await this.toastHandler.PopupToast(new ToastDetail
        {
            Title = "Hack System",
            AutoHide = false,
            Icon = ToastIcons.Error,
            Message = "Should be closed manually."
        });
        await this.toastHandler.PopupToast(new ToastDetail
        {
            Title = "Hack System",
            AutoHide = false,
            Icon = ToastIcons.HackSystem,
            Message = "Welcome to Hack System."
        });
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
                await this.ProgramDrawerComponent.LoadProgramDrawer(mapDetails);
                await this.ProgramDockComponent.LoadProgramDock(mapDetails);
            }
            else
            {
                this.ProgramDockComponent.ClearProgramDock();
                this.ProgramDrawerComponent.ClearProgramDrawer();
            }
            this.logger.LogInformation($"Query {maps.Count()} programs successfully.");
            await this.toastHandler.PopupToast(new ToastDetail
            {
                Title = "Ready to launch program.",
                Icon = ToastIcons.Information,
                Message = $"Having {maps.Count()} available programes, Enjoy your time!"
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, $"Query programs failed.");
            await this.toastHandler.PopupToast(new ToastDetail
            {
                Title = "Failed to query programes.",
                Icon = ToastIcons.Error,
                Message = "Failed to query available programes, please refresh this page!"
            });
        }
    }
}
