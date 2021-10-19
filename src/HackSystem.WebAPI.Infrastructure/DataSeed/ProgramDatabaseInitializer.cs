using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.Infrastructure.DataSeed;

public static class ProgramDatabaseInitializer
{
    public static ModelBuilder InitializeProgramData(this ModelBuilder builder)
    {
        builder.Entity<ProgramDetail>().HasData(new[]
        {
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home", IconUri="/images/ProgramIcons/HomeProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Home.HomeComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer", IconUri="/images/ProgramIcons/ExplorerProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Explorer.ExplorerComponent", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Borwser", IconUri="/images/ProgramIcons/BorwserProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Borwser.BorwserComponent", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Profile", IconUri="/images/ProgramIcons/ProfileProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Profile.ProfileComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "Setting", IconUri="/images/ProgramIcons/SettingProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Setting.SettingComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000006", Enabled = true, Name = "AppStore", IconUri="/images/ProgramIcons/AppStoreProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.AppStore.AppStoreComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000007", Enabled = true, Name = "Weather", IconUri="/images/ProgramIcons/WeatherProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.Weather.WeatherComponent", SingleInstance=true, Mandatory = false },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000008", Enabled = true, Name = "TaskServer", IconUri="/images/ProgramIcons/TaskServerProgram.png", EntryAssemblyName="HackSystem.Web.TaskSchedule", EntryTypeName="HackSystem.Web.TaskSchedule.Launcher", SingleInstance=true, Mandatory = true, EntryParameter = "{ \"Developer\": \"Leon\" }" },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000009", Enabled = true, Name = "MockServer", IconUri="/images/ProgramIcons/MockServerProgram.png", EntryAssemblyName="HackSystem.Web.SystemProgram", EntryTypeName="HackSystem.Web.SystemProgram.MockServer.MockServerComponent", SingleInstance=true, Mandatory = true },
        });
        return builder;
    }
}
