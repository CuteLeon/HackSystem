using HackSystem.WebAPI.Domain.Entity.Identity;
using HackSystem.WebAPI.ProgramServer.Domain.Entity;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using Microsoft.AspNetCore.Identity;

namespace HackSystem.WebAPI.Infrastructure.DataSeed;

public static class IdentityDatabaseInitializer
{
    public static ModelBuilder InitializeIdentityData(this ModelBuilder builder)
    {
        builder.Entity<HackSystemRole>().HasData(new[]
        {
            new HackSystemRole() { Id = "msaspnet-core-role-hack-system000001", Name = "Hacker", NormalizedName = "HACKER", ConcurrencyStamp = "8ef3768d-cdd3-43a4-9338-c549cec56942" },
            new HackSystemRole() { Id = "msaspnet-core-role-hack-system000002", Name = "Commander", NormalizedName = "COMMANDER", ConcurrencyStamp = "43daf209-df6b-499c-83e5-94ea05cf8997" }
        });

        builder.Entity<HackSystemUser>().HasData(new[]
        {
            new HackSystemUser() { Id = "msaspnet-core-user-hack-system000001", UserName = "Leon", NormalizedUserName = "LEON", Email = "leon@hack.com", NormalizedEmail = "LEON@HACK.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEBpsyxgzjSNJvSIm6y3I1jqvKN4iV/IkvwmMrrYR5X8a6pEXza2RwA9xxSXidOiGkQ==", SecurityStamp = "SU6NODNYTSGYJ5NXXYIA7I2M542MLV2V", ConcurrencyStamp = "baeb86b5-116c-43ae-ade7-489dabd07012", PhoneNumber = null, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0 },
            new HackSystemUser() { Id = "msaspnet-core-user-hack-system000002", UserName = "CMD", NormalizedUserName = "CMD", Email = "commander@hack.com", NormalizedEmail = "COMMANDER@HACK.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEBLD9HIQLb2pLRH1Vrv1PnOuab+diYEwtCoWFyIx/S+C2nynO4S9NMBUjdQUcVWFrg==", SecurityStamp = "GMGJIR7MWWVSLB2IYMAP445FXWPKZQE5", ConcurrencyStamp = "a3e94fcc-39cf-4a2f-8b23-f08424042cb8", PhoneNumber = null, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0 },
            new HackSystemUser() { Id = "msaspnet-core-user-hack-system000003", UserName = "Mathilda", NormalizedUserName = "MATHILDA", Email = "mathilda@hack.com", NormalizedEmail = "MATHILDA@HACK.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEDjIsjVamUxv4OQ06Ur/7YnsqddYfO2eQP7UK/Adjs38RIkmBpgTldrfCXZ5QHP1vQ==", SecurityStamp = "2NGFUDFGMLPCBN5U67CHXJEYIDBWQPO3", ConcurrencyStamp = "93cdc1b8-0c84-4f52-9245-d6ae4bbe5f59", PhoneNumber = null, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnd = null, LockoutEnabled = true, AccessFailedCount = 0 }
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new[]
        {
            new IdentityUserRole<string>() { UserId = "msaspnet-core-user-hack-system000001", RoleId = "msaspnet-core-role-hack-system000001" },
            new IdentityUserRole<string>() { UserId = "msaspnet-core-user-hack-system000001", RoleId = "msaspnet-core-role-hack-system000002" },
            new IdentityUserRole<string>() { UserId = "msaspnet-core-user-hack-system000002", RoleId = "msaspnet-core-role-hack-system000002" },
            new IdentityUserRole<string>() { UserId = "msaspnet-core-user-hack-system000003", RoleId = "msaspnet-core-role-hack-system000001" },
        });

        builder.Entity<IdentityUserClaim<string>>().HasData(new[]
        {
            new IdentityUserClaim<string> { Id = 1, UserId = "msaspnet-core-user-hack-system000001", ClaimType = "Professional", ClaimValue = "true" }
        });

        builder.Entity<ProgramUser>().HasData(new[]
        {
            new ProgramUser() { Id = "msaspnet-core-user-hack-system000001" },
            new ProgramUser() { Id = "msaspnet-core-user-hack-system000002" },
            new ProgramUser() { Id = "msaspnet-core-user-hack-system000003" }
        });

        builder.Entity<UserProgramMap>().HasData(new[]
        {
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000001", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000002", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000003", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000004", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000005", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000006", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000001", ProgramId = "program0-icon-0828-hack-system000007", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },

            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000001", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000002", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000003", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000004", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000005", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000006", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000002", ProgramId = "program0-icon-0828-hack-system000007", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },

            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000001", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000002", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000003", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000004", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000005", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = true },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000006", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
            new UserProgramMap { UserId = "msaspnet-core-user-hack-system000003", ProgramId = "program0-icon-0828-hack-system000007", Rename = null, PinToTop = false, PinToDesktop=true, PinToDock = false },
        });

        return builder;
    }
}
