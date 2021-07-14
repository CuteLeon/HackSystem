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
                .HasAnnotation("ProductVersion", "6.0.0-preview.1.21102.2");

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

                    b.ToTable("AspNetRoles");
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

                    b.ToTable("AspNetUsers");
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

                    b.HasIndex(new[] { "UserId", "ProgramId" }, "UserBasicProgramMap_Index");

                    b.HasIndex(new[] { "UserId" }, "UserBasicProgramMap_UserId_Index");

                    b.ToTable("UserBasicProgramMaps");
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

                    b.HasIndex(new[] { "Id", "Name" }, "BasicProgram_Index");

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

                    b.ToTable("AspNetRoleClaims");
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

                    b.ToTable("AspNetUserClaims");
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

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
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

                    b.ToTable("AspNetUserTokens");
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