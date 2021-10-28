using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.Infrastructure.DataSeed;

public static class ProgramDatabaseInitializer
{
    public static ModelBuilder InitializeProgramData(this ModelBuilder builder)
    {
        builder.Entity<ProgramDetail>().HasData(new[]
        {
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home", EntryAssemblyName="HackSystem.Web.Home", EntryTypeName="HackSystem.Web.Home.Launcher", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer", EntryAssemblyName="HackSystem.Web.Explorer", EntryTypeName="HackSystem.Web.Explorer.Launcher", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Profile", EntryAssemblyName="HackSystem.Web.Profile", EntryTypeName="HackSystem.Web.Profile.Launcher", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Setting", EntryAssemblyName="HackSystem.Web.Setting", EntryTypeName="HackSystem.Web.Setting.Launcher", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "AppStore", EntryAssemblyName="HackSystem.Web.AppStore", EntryTypeName="HackSystem.Web.AppStore.Launcher", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000006", Enabled = true, Name = "TaskServer", EntryAssemblyName="HackSystem.Web.TaskSchedule", EntryTypeName="HackSystem.Web.TaskSchedule.Launcher", SingleInstance=true, Mandatory = true, EntryParameter = "{ \"Developer\": \"Leon\" }" },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000007", Enabled = true, Name = "MockServer", EntryAssemblyName="HackSystem.Web.MockServer", EntryTypeName="HackSystem.Web.MockServer.Launcher", SingleInstance=true, Mandatory = true },
        });
        return builder;
    }
}
