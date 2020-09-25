using HackSystem.WebAPI.Model.Program;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackSystem.WebAPI.DataAccess.DataSeed
{
    public static class ProgramDatabaseInitializer
    {
        public static DataBuilder<BasicProgram> InitializeBasicProgramData(this ModelBuilder builder)
        {
            return builder.Entity<BasicProgram>().HasData(new[]
            {
                new BasicProgram() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home", IconUri="/image/Icon/HomeProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.Home.HomeComponent" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer", IconUri="/image/Icon/ExplorerProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.Explorer.ExplorerComponent" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Borwser", IconUri="/image/Icon/BorwserProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.Borwser.BorwserComponent" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Profile", IconUri="/image/Icon/ProfileProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.Profile.ProfileComponent" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "Setting", IconUri="/image/Icon/SettingProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.Setting.SettingComponent" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000006", Enabled = true, Name = "AppStore", IconUri="/image/Icon/AppStoreProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.AppStore.AppStoreComponent" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000007", Enabled = true, Name = "Weather", IconUri="/image/Icon/WeatherProgram.png", AssemblyTypeFullName="HackSystem.Web.SystemProgram.Weather.WeatherComponent" }
            });
        }
    }
}
