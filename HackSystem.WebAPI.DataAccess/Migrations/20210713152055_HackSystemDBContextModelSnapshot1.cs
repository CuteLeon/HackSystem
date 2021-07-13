using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBContextModelSnapshot1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicPrograms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IconUri = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSingleton = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssemblyName = table.Column<string>(type: "TEXT", nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: true),
                    Integral = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBasicProgramMaps",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProgramId = table.Column<string>(type: "TEXT", nullable: false),
                    Hide = table.Column<bool>(type: "INTEGER", nullable: false),
                    PinToDock = table.Column<bool>(type: "INTEGER", nullable: false),
                    PinToTop = table.Column<bool>(type: "INTEGER", nullable: false),
                    Rename = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBasicProgramMaps", x => new { x.UserId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_UserBasicProgramMaps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBasicProgramMaps_BasicPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "BasicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000001", "HackSystem.Web.SystemProgram", true, "/image/Icon/HomeProgram.png", true, true, "Home", "HackSystem.Web.SystemProgram.Home.HomeComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000002", "HackSystem.Web.SystemProgram", true, "/image/Icon/ExplorerProgram.png", true, false, "Explorer", "HackSystem.Web.SystemProgram.Explorer.ExplorerComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000003", "HackSystem.Web.SystemProgram", true, "/image/Icon/BorwserProgram.png", false, false, "Borwser", "HackSystem.Web.SystemProgram.Borwser.BorwserComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000004", "HackSystem.Web.SystemProgram", true, "/image/Icon/ProfileProgram.png", true, true, "Profile", "HackSystem.Web.SystemProgram.Profile.ProfileComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000005", "HackSystem.Web.SystemProgram", true, "/image/Icon/SettingProgram.png", true, true, "Setting", "HackSystem.Web.SystemProgram.Setting.SettingComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000006", "HackSystem.Web.SystemProgram", true, "/image/Icon/AppStoreProgram.png", true, true, "AppStore", "HackSystem.Web.SystemProgram.AppStore.AppStoreComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000007", "HackSystem.Web.SystemProgram", true, "/image/Icon/WeatherProgram.png", false, true, "Weather", "HackSystem.Web.SystemProgram.Weather.WeatherComponent" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "BasicProgram_Index",
                table: "BasicPrograms",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_UserBasicProgramMaps_ProgramId",
                table: "UserBasicProgramMaps",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "UserBasicProgramMap_Index",
                table: "UserBasicProgramMaps",
                columns: new[] { "UserId", "ProgramId" });

            migrationBuilder.CreateIndex(
                name: "UserBasicProgramMap_UserId_Index",
                table: "UserBasicProgramMaps",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "UserBasicProgramMaps");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BasicPrograms");
        }
    }
}
