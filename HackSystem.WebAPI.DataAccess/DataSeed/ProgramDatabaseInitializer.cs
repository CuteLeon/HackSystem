using HackSystem.WebAPI.Model.Program;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.DataAccess.DataSeed
{
    public static class ProgramDatabaseInitializer
    {
        public static ModelBuilder InitializeBasicProgramData(this ModelBuilder builder)
        {
            builder.Entity<BasicProgram>().HasData(new[]
            {
                new BasicProgram() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home", IconUri="/image/Icon/HomeProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Home.HomeComponent", IsSingleton=true, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer", IconUri="/image/Icon/ExplorerProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Explorer.ExplorerComponent", IsSingleton=false, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Borwser", IconUri="/image/Icon/BorwserProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Borwser.BorwserComponent", IsSingleton=false, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Profile", IconUri="/image/Icon/ProfileProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Profile.ProfileComponent", IsSingleton=true, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "Setting", IconUri="/image/Icon/SettingProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Setting.SettingComponent", IsSingleton=true, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000006", Enabled = true, Name = "AppStore", IconUri="/image/Icon/AppStoreProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.AppStore.AppStoreComponent", IsSingleton=true, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000007", Enabled = true, Name = "Weather", IconUri="/image/Icon/WeatherProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.Weather.WeatherComponent", IsSingleton=true, Integral = false },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000008", Enabled = true, Name = "TaskServer", IconUri="/image/Icon/TaskServerProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.TaskServer.TaskServerComponent", IsSingleton=true, Integral = true },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000009", Enabled = true, Name = "MockServer", IconUri="/image/Icon/MockServerProgram.png", AssemblyName="HackSystem.Web.SystemProgram", TypeName="HackSystem.Web.SystemProgram.MockServer.MockServerComponent", IsSingleton=true, Integral = true },
            });
            return builder;
        }
    }
}
