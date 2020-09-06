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
                new BasicProgram() { Enabled = true, Name = "Home" },
                new BasicProgram() { Enabled = true, Name = "Explorer" },
                new BasicProgram() { Enabled = true, Name = "Borwser" },
                new BasicProgram() { Enabled = true, Name = "Profile" },
                new BasicProgram() { Enabled = true, Name = "Setting" }
            });
        }
    }
}
