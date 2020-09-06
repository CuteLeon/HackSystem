using HackSystem.WebAPI.DataAccess.DataSeed;
using HackSystem.WebAPI.Model.Identity;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Model.Program;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.DataAccess
{
    public class HackSystemDBContext : IdentityDbContext<HackSystemUser, HackSystemRole, string>
    {
        public HackSystemDBContext() { }

        public HackSystemDBContext(DbContextOptions<HackSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BasicProgram> BasicPrograms { get; set; }

        public virtual DbSet<UserProgramMap> UserProgramMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserProgramMap>().HasOne(map => map.Program).WithMany(program => program.UserProgramMaps).HasForeignKey(map => map.ProgramId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserProgramMap>().HasOne(map => map.User).WithMany(user => user.UserProgramMaps).HasForeignKey(map => map.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BasicProgram>().HasIndex(program => new { program.Id, program.Name }, $"{nameof(BasicProgram)}_Index");
            builder.Entity<UserProgramMap>().HasIndex(map => new { map.Id, map.UserId }, $"{nameof(UserProgramMap)}_Index");

            builder.InitializeBasicProgramData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 这个和无参构造函数用于设计时生成数据库迁移
            base.OnConfiguring(optionsBuilder.UseSqlite("DATA SOURCE=HackSystem.db"));
        }
    }
}
