using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.Infrastructure.DataSeed;

public static class ProgramDatabaseInitializer
{
    public static ModelBuilder InitializeProgramData(this ModelBuilder builder)
    {
        builder.Entity<ProgramDetail>().HasData(new[]
        {
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Home.HomeComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Explorer.ExplorerComponent", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Borwser", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Borwser.BorwserComponent", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Profile", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Profile.ProfileComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "Setting", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Setting.SettingComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000006", Enabled = true, Name = "AppStore", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.AppStore.AppStoreComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000007", Enabled = true, Name = "Weather", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Weather.WeatherComponent", SingleInstance=true, Mandatory = false },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000008", Enabled = true, Name = "TaskServer", EntryAssemblyName="HackSystem.Web.TaskSchedule", EntryTypeName="HackSystem.Web.TaskSchedule.Launcher", SingleInstance=true, Mandatory = true, EntryParameter = "{ \"Developer\": \"Leon\" }" },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000009", Enabled = true, Name = "MockServer", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.MockServer.MockServerComponent", SingleInstance=true, Mandatory = true },
        });
        return builder;
    }
}
