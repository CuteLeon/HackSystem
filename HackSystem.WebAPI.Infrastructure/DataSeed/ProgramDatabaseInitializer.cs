using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.Infrastructure.DataSeed;

public static class ProgramDatabaseInitializer
{
    public static ModelBuilder InitializeProgramData(this ModelBuilder builder)
    {
        builder.Entity<ProgramDetail>().HasData(new[]
        {
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home", IconUri="/images/ProgramIcons/HomeProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Home.HomeComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer", IconUri="/images/ProgramIcons/ExplorerProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Explorer.ExplorerComponent", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Borwser", IconUri="/images/ProgramIcons/BorwserProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Borwser.BorwserComponent", SingleInstance=false, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Profile", IconUri="/images/ProgramIcons/ProfileProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Profile.ProfileComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "Setting", IconUri="/images/ProgramIcons/SettingProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Setting.SettingComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000006", Enabled = true, Name = "AppStore", IconUri="/images/ProgramIcons/AppStoreProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.AppStore.AppStoreComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000007", Enabled = true, Name = "Weather", IconUri="/images/ProgramIcons/WeatherProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Weather.WeatherComponent", SingleInstance=true, Mandatory = false },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000008", Enabled = true, Name = "TaskServer", IconUri="/images/ProgramIcons/TaskServerProgram.png", AssemblyName="HackSystem.Web.TaskSchedule", TypeName="HackSystem.Web.TaskSchedule.TaskSchedulerComponent", SingleInstance=true, Mandatory = true },
            new ProgramDetail() { Id = "program0-icon-0828-hack-system000009", Enabled = true, Name = "MockServer", IconUri="/images/ProgramIcons/MockServerProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.MockServer.MockServerComponent", SingleInstance=true, Mandatory = true },
        });
        return builder;
    }
}
