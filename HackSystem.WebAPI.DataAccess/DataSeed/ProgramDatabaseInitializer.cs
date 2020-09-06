using System;
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
                new BasicProgram() { Id = Guid.NewGuid().ToString("N"), Enabled = true, Name = "Home" },
                new BasicProgram() { Id = Guid.NewGuid().ToString("N"), Enabled = true, Name = "Explorer" },
                new BasicProgram() { Id = Guid.NewGuid().ToString("N"), Enabled = true, Name = "Borwser" },
                new BasicProgram() { Id = Guid.NewGuid().ToString("N"), Enabled = true, Name = "Profile" },
                new BasicProgram() { Id = Guid.NewGuid().ToString("N"), Enabled = true, Name = "Setting" }
            });
        }
    }
}
