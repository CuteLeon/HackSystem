using HackSystem.WebAPI.DataAccess.DataSeed;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Model.Program;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.DataAccess
{
    public class HackSystemDBContext : IdentityDbContext<HackSystemUser, HackSystemRole, string>
    {
        private readonly ILogger<HackSystemDBContext> logger;

        public HackSystemDBContext() { }

        public HackSystemDBContext(DbContextOptions<HackSystemDBContext> options)
            : base(options)
        {
        }

        public HackSystemDBContext(
            ILogger<HackSystemDBContext> logger,
            DbContextOptions<HackSystemDBContext> options)
            : base(options)
        {
            this.logger = logger;
        }

        public virtual DbSet<BasicProgram> BasicPrograms { get; set; }

        public virtual DbSet<UserBasicProgramMap> UserBasicProgramMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserBasicProgramMap>().HasOne(map => map.BasicProgram).WithMany(program => program.UserProgramMaps).HasForeignKey(map => map.ProgramId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserBasicProgramMap>().HasOne(map => map.User).WithMany(user => user.UserProgramMaps).HasForeignKey(map => map.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserBasicProgramMap>().HasKey(map => new { map.UserId, map.ProgramId });

            builder.Entity<BasicProgram>().HasIndex(program => new { program.Id, program.Name }, $"{nameof(BasicProgram)}_Index");
            builder.Entity<UserBasicProgramMap>().HasIndex(map => map.UserId, $"{nameof(UserBasicProgramMap)}_{nameof(UserBasicProgramMap.UserId)}_Index");
            builder.Entity<UserBasicProgramMap>().HasIndex(map => new { map.UserId, map.ProgramId }, $"{nameof(UserBasicProgramMap)}_Index");

            builder.InitializeBasicProgramData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(this.Log);

            // Use to generate database migrations when design time
            base.OnConfiguring(optionsBuilder.UseSqlite("DATA SOURCE=HackSystem.db"));
        }

        private void Log(string message)
            => this.logger.LogDebug(message);
    }
}
