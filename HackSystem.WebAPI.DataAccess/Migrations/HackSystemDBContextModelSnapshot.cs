﻿// <auto-generated />
using System;
using HackSystem.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    [DbContext(typeof(HackSystemDBContext))]
    partial class HackSystemDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-preview.6.21352.1");

            modelBuilder.Entity("HackSystem.WebAPI.Model.Identity.HackSystemRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Identity.HackSystemUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Map.UserMap.UserBasicProgramMap", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProgramId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Hide")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PinToDock")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PinToTop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rename")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ProgramId");

                    b.HasIndex("ProgramId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId", "ProgramId")
                        .IsUnique();

                    b.ToTable("UserBasicProgramMaps");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Mock.MockRouteDetail", b =>
                {
                    b.Property<int>("RouteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DelayDuration")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForwardAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("ForwardMethod")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForwardMockType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForwardRequestBodyTemplate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MockMethod")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<string>("MockSourceHost")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<int>("MockType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MockURI")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<string>("ResponseBodyTemplate")
                        .HasColumnType("TEXT");

                    b.Property<string>("RouteName")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusCode")
                        .HasColumnType("INTEGER");

                    b.HasKey("RouteID");

                    b.HasIndex("Enabled");

                    b.HasIndex("MockURI", "MockMethod", "MockSourceHost")
                        .IsUnique();

                    b.ToTable("MockRouteDetails");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Mock.MockRouteLogDetail", b =>
                {
                    b.Property<int>("RouteLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConnectionID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FinishDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ForwardAddress")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ForwardDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ForwardMethod")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForwardMockType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ForwardRequestBodyTemplate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ForwardResponseBody")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForwardResponseStatusCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Method")
                        .HasColumnType("TEXT");

                    b.Property<int>("MockRouteLogStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MockType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequestBody")
                        .HasColumnType("TEXT");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("TEXT");

                    b.Property<int>("RouteID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourceHost")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("URI")
                        .HasColumnType("TEXT");

                    b.HasKey("RouteLogID");

                    b.HasIndex("RouteID");

                    b.HasIndex("URI", "Method", "SourceHost", "MockType");

                    b.ToTable("MockRouteLogDetails");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Option.GenericOption", b =>
                {
                    b.Property<int>("OptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("OptionName")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<string>("OptionValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerLevel")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.HasKey("OptionID");

                    b.HasIndex("OptionName");

                    b.HasIndex("OwnerLevel", "OptionName");

                    b.HasIndex("OwnerLevel", "Category", "OptionName")
                        .IsUnique();

                    b.ToTable("GenericOptions");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Program.BasicProgram", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IconUri")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Integral")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSingleton")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id", "Name");

                    b.ToTable("BasicPrograms");

                    b.HasData(
                        new
                        {
                            Id = "program0-icon-0828-hack-system000001",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/HomeProgram.png",
                            Integral = true,
                            IsSingleton = true,
                            Name = "Home",
                            TypeName = "HackSystem.Web.SystemProgram.Home.HomeComponent"
                        },
                        new
                        {
                            Id = "program0-icon-0828-hack-system000002",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/ExplorerProgram.png",
                            Integral = true,
                            IsSingleton = false,
                            Name = "Explorer",
                            TypeName = "HackSystem.Web.SystemProgram.Explorer.ExplorerComponent"
                        },
                        new
                        {
                            Id = "program0-icon-0828-hack-system000003",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/BorwserProgram.png",
                            Integral = false,
                            IsSingleton = false,
                            Name = "Borwser",
                            TypeName = "HackSystem.Web.SystemProgram.Borwser.BorwserComponent"
                        },
                        new
                        {
                            Id = "program0-icon-0828-hack-system000004",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/ProfileProgram.png",
                            Integral = true,
                            IsSingleton = true,
                            Name = "Profile",
                            TypeName = "HackSystem.Web.SystemProgram.Profile.ProfileComponent"
                        },
                        new
                        {
                            Id = "program0-icon-0828-hack-system000005",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/SettingProgram.png",
                            Integral = true,
                            IsSingleton = true,
                            Name = "Setting",
                            TypeName = "HackSystem.Web.SystemProgram.Setting.SettingComponent"
                        },
                        new
                        {
                            Id = "program0-icon-0828-hack-system000006",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/AppStoreProgram.png",
                            Integral = true,
                            IsSingleton = true,
                            Name = "AppStore",
                            TypeName = "HackSystem.Web.SystemProgram.AppStore.AppStoreComponent"
                        },
                        new
                        {
                            Id = "program0-icon-0828-hack-system000007",
                            AssemblyName = "HackSystem.Web.SystemProgram",
                            Enabled = true,
                            IconUri = "/image/Icon/WeatherProgram.png",
                            Integral = false,
                            IsSingleton = true,
                            Name = "Weather",
                            TypeName = "HackSystem.Web.SystemProgram.Weather.WeatherComponent"
                        });
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Task.TaskDetail", b =>
                {
                    b.Property<int>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("AutomaticInterval")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExecuteDateTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("FirstInterval")
                        .HasColumnType("TEXT");

                    b.Property<string>("Parameters")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProcedureName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Reentrant")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TaskFrequency")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TaskName")
                        .HasColumnType("TEXT");

                    b.HasKey("TaskID");

                    b.HasIndex("Enabled");

                    b.HasIndex("TaskName")
                        .IsUnique();

                    b.HasIndex("TaskName", "ExecuteDateTime");

                    b.ToTable("TaskDetails");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Task.TaskLogDetail", b =>
                {
                    b.Property<int>("TaskLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Exception")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FinishDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Parameters")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TaskID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TaskLogStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Trigger")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TriggerDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("TaskLogID");

                    b.HasIndex("TaskID");

                    b.HasIndex("TaskID", "TaskLogStatus");

                    b.ToTable("TaskLogDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Map.UserMap.UserBasicProgramMap", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Program.BasicProgram", "BasicProgram")
                        .WithMany("UserProgramMaps")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemUser", "User")
                        .WithMany("UserProgramMaps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BasicProgram");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Mock.MockRouteLogDetail", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Mock.MockRouteDetail", null)
                        .WithMany()
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Task.TaskLogDetail", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Task.TaskDetail", null)
                        .WithMany()
                        .HasForeignKey("TaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HackSystem.WebAPI.Model.Identity.HackSystemUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Identity.HackSystemUser", b =>
                {
                    b.Navigation("UserProgramMaps");
                });

            modelBuilder.Entity("HackSystem.WebAPI.Model.Program.BasicProgram", b =>
                {
                    b.Navigation("UserProgramMaps");
                });
#pragma warning restore 612, 618
        }
    }
}
