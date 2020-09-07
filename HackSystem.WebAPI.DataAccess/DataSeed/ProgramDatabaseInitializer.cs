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
                new BasicProgram() { Id = "program0-icon-0828-hack-system000001", Enabled = true, Name = "Home" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000002", Enabled = true, Name = "Explorer" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000003", Enabled = true, Name = "Borwser" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000004", Enabled = true, Name = "Profile" },
                new BasicProgram() { Id = "program0-icon-0828-hack-system000005", Enabled = true, Name = "Setting" }
            });
        }
    }
}
