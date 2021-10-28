using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackSystem.WebAPI.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenericOptions",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionName = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    OptionValue = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    OwnerLevel = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericOptions", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "MockRouteDetails",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteName = table.Column<string>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    MockURI = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    MockMethod = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    MockSourceHost = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    DelayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponseBodyTemplate = table.Column<string>(type: "TEXT", nullable: false),
                    MockType = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMethod = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardRequestBodyTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMockType = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockRouteDetails", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    SingleInstance = table.Column<bool>(type: "INTEGER", nullable: false),
                    EntryAssemblyName = table.Column<string>(type: "TEXT", nullable: false),
                    EntryTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    EntryParameter = table.Column<string>(type: "TEXT", nullable: true),
                    Mandatory = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskName = table.Column<string>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExecuteDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    AutomaticInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TaskFrequency = table.Column<int>(type: "INTEGER", nullable: false),
                    Reentrant = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssemblyName = table.Column<string>(type: "TEXT", nullable: false),
                    ClassName = table.Column<string>(type: "TEXT", nullable: false),
                    ProcedureName = table.Column<string>(type: "TEXT", nullable: false),
                    Parameters = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "WebAPILogs",
                columns: table => new
                {
                    WebAPILogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestURI = table.Column<string>(type: "TEXT", nullable: false),
                    QueryString = table.Column<string>(type: "TEXT", nullable: false),
                    Method = table.Column<string>(type: "TEXT", nullable: false),
                    SourceHost = table.Column<string>(type: "TEXT", nullable: false),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: false),
                    TraceIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    IsAuthenticated = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdentityName = table.Column<string>(type: "TEXT", nullable: true),
                    RequestBody = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ElapsedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    Exception = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebAPILogs", x => x.WebAPILogID);
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
                name: "MockRouteLogDetails",
                columns: table => new
                {
                    RouteLogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteID = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectionID = table.Column<string>(type: "TEXT", nullable: false),
                    URI = table.Column<string>(type: "TEXT", nullable: false),
                    Method = table.Column<string>(type: "TEXT", nullable: false),
                    SourceHost = table.Column<string>(type: "TEXT", nullable: false),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestBody = table.Column<string>(type: "TEXT", nullable: false),
                    ResponseBody = table.Column<string>(type: "TEXT", nullable: false),
                    MockType = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMethod = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardResponseStatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMockType = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ForwardDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FinishDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MockRouteLogStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Exception = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockRouteLogDetails", x => x.RouteLogID);
                    table.ForeignKey(
                        name: "FK_MockRouteLogDetails_MockRouteDetails_RouteID",
                        column: x => x.RouteID,
                        principalTable: "MockRouteDetails",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    ExperiencePoints = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ProgramUsers_Id",
                        column: x => x.Id,
                        principalTable: "ProgramUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgramMaps",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProgramId = table.Column<string>(type: "TEXT", nullable: false),
                    PinToDesktop = table.Column<bool>(type: "INTEGER", nullable: false),
                    PinToDock = table.Column<bool>(type: "INTEGER", nullable: false),
                    PinToTop = table.Column<bool>(type: "INTEGER", nullable: false),
                    Rename = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgramMaps", x => new { x.UserId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_UserProgramMaps_ProgramDetails_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProgramMaps_ProgramUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ProgramUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskLogDetails",
                columns: table => new
                {
                    TaskLogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskID = table.Column<int>(type: "INTEGER", nullable: false),
                    Parameters = table.Column<string>(type: "TEXT", nullable: true),
                    TriggerDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TaskLogStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Trigger = table.Column<string>(type: "TEXT", nullable: false),
                    Exception = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLogDetails", x => x.TaskLogID);
                    table.ForeignKey(
                        name: "FK_TaskLogDetails_TaskDetails_TaskID",
                        column: x => x.TaskID,
                        principalTable: "TaskDetails",
                        principalColumn: "TaskID",
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "msaspnet-core-role-hack-system000001", "8ef3768d-cdd3-43a4-9338-c549cec56942", "Hacker", "HACKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "msaspnet-core-role-hack-system000002", "43daf209-df6b-499c-83e5-94ea05cf8997", "Commander", "COMMANDER" });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000001", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.Home.HomeComponent", true, "Home", true });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000002", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.Explorer.ExplorerComponent", true, "Explorer", false });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000003", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.Borwser.BorwserComponent", true, "Borwser", false });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000004", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.Profile.ProfileComponent", true, "Profile", true });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000005", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.Setting.SettingComponent", true, "Setting", true });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000006", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.AppStore.AppStoreComponent", true, "AppStore", true });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000007", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.Weather.WeatherComponent", false, "Weather", true });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000008", true, "HackSystem.Web.TaskSchedule", "{ \"Developer\": \"Leon\" }", "HackSystem.Web.TaskSchedule.Launcher", true, "TaskServer", true });

            migrationBuilder.InsertData(
                table: "ProgramDetails",
                columns: new[] { "Id", "Enabled", "EntryAssemblyName", "EntryParameter", "EntryTypeName", "Mandatory", "Name", "SingleInstance" },
                values: new object[] { "program0-icon-0828-hack-system000009", true, "HackSystem.Web.SystemProgram", null, "HackSystem.Web.SystemProgram.MockServer.MockServerComponent", true, "MockServer", true });

            migrationBuilder.InsertData(
                table: "ProgramUsers",
                column: "Id",
                value: "msaspnet-core-user-hack-system000001");

            migrationBuilder.InsertData(
                table: "ProgramUsers",
                column: "Id",
                value: "msaspnet-core-user-hack-system000002");

            migrationBuilder.InsertData(
                table: "ProgramUsers",
                column: "Id",
                value: "msaspnet-core-user-hack-system000003");

            migrationBuilder.InsertData(
                table: "TaskDetails",
                columns: new[] { "TaskID", "AssemblyName", "AutomaticInterval", "Category", "ClassName", "CreateTime", "Enabled", "ExecuteDateTime", "FirstInterval", "Parameters", "ProcedureName", "Reentrant", "TaskFrequency", "TaskName" },
                values: new object[] { 1, "HackSystem.WebAPI.Tasks", new TimeSpan(0, 0, 0, 0, 0), "HackSystem", "HackSystem.WebAPI.Tasks.DatabaseBackup.IDatabaseBackupTask", new DateTime(2021, 7, 24, 14, 3, 30, 0, DateTimeKind.Unspecified), true, new DateTime(2021, 7, 24, 5, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0), "OriginDB=DATA SOURCE=.\\HackSystem.db|BackupDB=DATA SOURCE=.\\HackSystem_backup_{0}.db", "Execute", false, 3, "Database Auto Backup" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExperienceLevel", "ExperiencePoints", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "msaspnet-core-user-hack-system000001", 0, "baeb86b5-116c-43ae-ade7-489dabd07012", "leon@hack.com", true, 0, 0, true, null, "LEON@HACK.COM", "LEON", "AQAAAAEAACcQAAAAEBpsyxgzjSNJvSIm6y3I1jqvKN4iV/IkvwmMrrYR5X8a6pEXza2RwA9xxSXidOiGkQ==", null, false, "SU6NODNYTSGYJ5NXXYIA7I2M542MLV2V", false, "Leon" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExperienceLevel", "ExperiencePoints", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "msaspnet-core-user-hack-system000002", 0, "a3e94fcc-39cf-4a2f-8b23-f08424042cb8", "commander@hack.com", true, 0, 0, true, null, "COMMANDER@HACK.COM", "CMD", "AQAAAAEAACcQAAAAEBLD9HIQLb2pLRH1Vrv1PnOuab+diYEwtCoWFyIx/S+C2nynO4S9NMBUjdQUcVWFrg==", null, false, "GMGJIR7MWWVSLB2IYMAP445FXWPKZQE5", false, "CMD" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ExperienceLevel", "ExperiencePoints", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "msaspnet-core-user-hack-system000003", 0, "93cdc1b8-0c84-4f52-9245-d6ae4bbe5f59", "mathilda@hack.com", true, 0, 0, true, null, "MATHILDA@HACK.COM", "MATHILDA", "AQAAAAEAACcQAAAAEDjIsjVamUxv4OQ06Ur/7YnsqddYfO2eQP7UK/Adjs38RIkmBpgTldrfCXZ5QHP1vQ==", null, false, "2NGFUDFGMLPCBN5U67CHXJEYIDBWQPO3", false, "Mathilda" });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000001", "msaspnet-core-user-hack-system000001", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000002", "msaspnet-core-user-hack-system000001", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000003", "msaspnet-core-user-hack-system000001", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000004", "msaspnet-core-user-hack-system000001", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000005", "msaspnet-core-user-hack-system000001", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000006", "msaspnet-core-user-hack-system000001", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000007", "msaspnet-core-user-hack-system000001", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000008", "msaspnet-core-user-hack-system000001", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000009", "msaspnet-core-user-hack-system000001", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000001", "msaspnet-core-user-hack-system000002", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000002", "msaspnet-core-user-hack-system000002", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000003", "msaspnet-core-user-hack-system000002", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000004", "msaspnet-core-user-hack-system000002", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000005", "msaspnet-core-user-hack-system000002", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000006", "msaspnet-core-user-hack-system000002", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000007", "msaspnet-core-user-hack-system000002", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000008", "msaspnet-core-user-hack-system000002", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000009", "msaspnet-core-user-hack-system000002", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000001", "msaspnet-core-user-hack-system000003", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000002", "msaspnet-core-user-hack-system000003", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000003", "msaspnet-core-user-hack-system000003", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000004", "msaspnet-core-user-hack-system000003", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000005", "msaspnet-core-user-hack-system000003", true, true, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000006", "msaspnet-core-user-hack-system000003", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000007", "msaspnet-core-user-hack-system000003", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000008", "msaspnet-core-user-hack-system000003", true, false, false, null });

            migrationBuilder.InsertData(
                table: "UserProgramMaps",
                columns: new[] { "ProgramId", "UserId", "PinToDesktop", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000009", "msaspnet-core-user-hack-system000003", true, false, false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "Professional", "true", "msaspnet-core-user-hack-system000001" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "msaspnet-core-role-hack-system000001", "msaspnet-core-user-hack-system000001" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "msaspnet-core-role-hack-system000002", "msaspnet-core-user-hack-system000001" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "msaspnet-core-role-hack-system000002", "msaspnet-core-user-hack-system000002" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "msaspnet-core-role-hack-system000001", "msaspnet-core-user-hack-system000003" });

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
                name: "IX_GenericOptions_OptionName_Category_OwnerLevel",
                table: "GenericOptions",
                columns: new[] { "OptionName", "Category", "OwnerLevel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MockRouteDetails_Enabled",
                table: "MockRouteDetails",
                column: "Enabled");

            migrationBuilder.CreateIndex(
                name: "IX_MockRouteDetails_MockURI_MockMethod_MockSourceHost",
                table: "MockRouteDetails",
                columns: new[] { "MockURI", "MockMethod", "MockSourceHost" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MockRouteLogDetails_RouteID",
                table: "MockRouteLogDetails",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_MockRouteLogDetails_URI_Method_SourceHost_MockType",
                table: "MockRouteLogDetails",
                columns: new[] { "URI", "Method", "SourceHost", "MockType" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetails_Id_Name",
                table: "ProgramDetails",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_Enabled",
                table: "TaskDetails",
                column: "Enabled");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskName",
                table: "TaskDetails",
                column: "TaskName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskName_ExecuteDateTime",
                table: "TaskDetails",
                columns: new[] { "TaskName", "ExecuteDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLogDetails_TaskID",
                table: "TaskLogDetails",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLogDetails_TaskID_TaskLogStatus",
                table: "TaskLogDetails",
                columns: new[] { "TaskID", "TaskLogStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProgramMaps_ProgramId",
                table: "UserProgramMaps",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgramMaps_UserId",
                table: "UserProgramMaps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgramMaps_UserId_ProgramId",
                table: "UserProgramMaps",
                columns: new[] { "UserId", "ProgramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebAPILogs_RequestURI_SourceHost_IdentityName_StartDateTime",
                table: "WebAPILogs",
                columns: new[] { "RequestURI", "SourceHost", "IdentityName", "StartDateTime" });
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
                name: "GenericOptions");

            migrationBuilder.DropTable(
                name: "MockRouteLogDetails");

            migrationBuilder.DropTable(
                name: "TaskLogDetails");

            migrationBuilder.DropTable(
                name: "UserProgramMaps");

            migrationBuilder.DropTable(
                name: "WebAPILogs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MockRouteDetails");

            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.DropTable(
                name: "ProgramDetails");

            migrationBuilder.DropTable(
                name: "ProgramUsers");
        }
    }
}
