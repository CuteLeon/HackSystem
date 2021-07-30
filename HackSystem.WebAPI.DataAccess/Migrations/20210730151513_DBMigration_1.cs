using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class DBMigration_1 : Migration
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
                name: "GenericOptions",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    OptionValue = table.Column<string>(type: "TEXT", nullable: true),
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
                    RouteName = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    MockURI = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    MockMethod = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    MockSourceHost = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    DelayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponseBodyTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    MockType = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMethod = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardRequestBodyTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMockType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockRouteDetails", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskName = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExecuteDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    AutomaticInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TaskFrequency = table.Column<int>(type: "INTEGER", nullable: false),
                    Reentrant = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssemblyName = table.Column<string>(type: "TEXT", nullable: true),
                    ClassName = table.Column<string>(type: "TEXT", nullable: true),
                    ProcedureName = table.Column<string>(type: "TEXT", nullable: true),
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
                    RequestURI = table.Column<string>(type: "TEXT", nullable: true),
                    QueryString = table.Column<string>(type: "TEXT", nullable: true),
                    Method = table.Column<string>(type: "TEXT", nullable: true),
                    SourceHost = table.Column<string>(type: "TEXT", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    TraceIdentifier = table.Column<string>(type: "TEXT", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "MockRouteLogDetails",
                columns: table => new
                {
                    RouteLogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteID = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectionID = table.Column<string>(type: "TEXT", nullable: true),
                    URI = table.Column<string>(type: "TEXT", nullable: true),
                    Method = table.Column<string>(type: "TEXT", nullable: true),
                    SourceHost = table.Column<string>(type: "TEXT", nullable: true),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestBody = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    MockType = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMethod = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardRequestBodyTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardResponseStatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMockType = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ForwardDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
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
                    Trigger = table.Column<string>(type: "TEXT", nullable: true),
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Level", "Name", "NormalizedName" },
                values: new object[] { "msaspnet-core-role-hack-system000001", "8ef3768d-cdd3-43a4-9338-c549cec56942", 0, "Hacker", "HACKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Level", "Name", "NormalizedName" },
                values: new object[] { "msaspnet-core-role-hack-system000002", "43daf209-df6b-499c-83e5-94ea05cf8997", 0, "Commander", "COMMANDER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "msaspnet-core-user-hack-system000001", 0, "baeb86b5-116c-43ae-ade7-489dabd07012", "leon@hack.com", true, 0, true, null, "LEON@HACK.COM", "LEON", "AQAAAAEAACcQAAAAEBpsyxgzjSNJvSIm6y3I1jqvKN4iV/IkvwmMrrYR5X8a6pEXza2RwA9xxSXidOiGkQ==", null, false, "SU6NODNYTSGYJ5NXXYIA7I2M542MLV2V", false, "Leon" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "msaspnet-core-user-hack-system000002", 0, "a3e94fcc-39cf-4a2f-8b23-f08424042cb8", "commander@hack.com", true, 0, true, null, "COMMANDER@HACK.COM", "CMD", "AQAAAAEAACcQAAAAEBLD9HIQLb2pLRH1Vrv1PnOuab+diYEwtCoWFyIx/S+C2nynO4S9NMBUjdQUcVWFrg==", null, false, "GMGJIR7MWWVSLB2IYMAP445FXWPKZQE5", false, "CMD" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "msaspnet-core-user-hack-system000003", 0, "93cdc1b8-0c84-4f52-9245-d6ae4bbe5f59", "mathilda@hack.com", true, 0, true, null, "MATHILDA@HACK.COM", "MATHILDA", "AQAAAAEAACcQAAAAEDjIsjVamUxv4OQ06Ur/7YnsqddYfO2eQP7UK/Adjs38RIkmBpgTldrfCXZ5QHP1vQ==", null, false, "2NGFUDFGMLPCBN5U67CHXJEYIDBWQPO3", false, "Mathilda" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000001", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/HomeProgram.png", true, true, "Home", "HackSystem.Web.SystemProgram.Home.HomeComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000002", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/ExplorerProgram.png", true, false, "Explorer", "HackSystem.Web.SystemProgram.Explorer.ExplorerComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000003", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/BorwserProgram.png", true, false, "Borwser", "HackSystem.Web.SystemProgram.Borwser.BorwserComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000004", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/ProfileProgram.png", true, true, "Profile", "HackSystem.Web.SystemProgram.Profile.ProfileComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000005", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/SettingProgram.png", true, true, "Setting", "HackSystem.Web.SystemProgram.Setting.SettingComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000006", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/AppStoreProgram.png", true, true, "AppStore", "HackSystem.Web.SystemProgram.AppStore.AppStoreComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000007", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/WeatherProgram.png", false, true, "Weather", "HackSystem.Web.SystemProgram.Weather.WeatherComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000008", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/TaskServerProgram.png", true, true, "TaskServer", "HackSystem.Web.SystemProgram.TaskServer.TaskServerComponent" });

            migrationBuilder.InsertData(
                table: "BasicPrograms",
                columns: new[] { "Id", "AssemblyName", "Enabled", "IconUri", "Integral", "IsSingleton", "Name", "TypeName" },
                values: new object[] { "program0-icon-0828-hack-system000009", "HackSystem.Web.SystemProgram", true, "/images/ProgramIcons/MockServerProgram.png", true, true, "MockServer", "HackSystem.Web.SystemProgram.MockServer.MockServerComponent" });

            migrationBuilder.InsertData(
                table: "TaskDetails",
                columns: new[] { "TaskID", "AssemblyName", "AutomaticInterval", "Category", "ClassName", "CreateTime", "Enabled", "ExecuteDateTime", "FirstInterval", "Parameters", "ProcedureName", "Reentrant", "TaskFrequency", "TaskName" },
                values: new object[] { 1, "HackSystem.WebAPI.Tasks", new TimeSpan(0, 0, 0, 0, 0), "HackSystem", "HackSystem.WebAPI.Tasks.DatabaseBackup.IDatabaseBackupTask", new DateTime(2021, 7, 24, 14, 3, 30, 0, DateTimeKind.Unspecified), true, new DateTime(2021, 7, 24, 5, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0), "OriginDB=DATA SOURCE=.\\HackSystem.db|BackupDB=DATA SOURCE=.\\HackSystem_backup_{0}.db", "Execute", false, 3, "Database Auto Backup" });

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

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000006", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000006", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000006", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000007", "msaspnet-core-user-hack-system000001", false, false, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000007", "msaspnet-core-user-hack-system000002", false, false, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000008", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000005", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000008", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000008", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000009", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000007", "msaspnet-core-user-hack-system000003", false, false, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000005", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000004", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000004", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000009", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000004", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000003", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000003", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000003", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000002", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000002", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000002", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000001", "msaspnet-core-user-hack-system000003", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000001", "msaspnet-core-user-hack-system000002", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000001", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000005", "msaspnet-core-user-hack-system000001", false, true, false, null });

            migrationBuilder.InsertData(
                table: "UserBasicProgramMaps",
                columns: new[] { "ProgramId", "UserId", "Hide", "PinToDock", "PinToTop", "Rename" },
                values: new object[] { "program0-icon-0828-hack-system000009", "msaspnet-core-user-hack-system000003", false, true, false, null });

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
                name: "IX_BasicPrograms_Id_Name",
                table: "BasicPrograms",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_GenericOptions_OptionName",
                table: "GenericOptions",
                column: "OptionName");

            migrationBuilder.CreateIndex(
                name: "IX_GenericOptions_OwnerLevel_Category_OptionName",
                table: "GenericOptions",
                columns: new[] { "OwnerLevel", "Category", "OptionName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenericOptions_OwnerLevel_OptionName",
                table: "GenericOptions",
                columns: new[] { "OwnerLevel", "OptionName" });

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
                name: "IX_UserBasicProgramMaps_ProgramId",
                table: "UserBasicProgramMaps",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBasicProgramMaps_UserId",
                table: "UserBasicProgramMaps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBasicProgramMaps_UserId_ProgramId",
                table: "UserBasicProgramMaps",
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
                name: "UserBasicProgramMaps");

            migrationBuilder.DropTable(
                name: "WebAPILogs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MockRouteDetails");

            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BasicPrograms");
        }
    }
}
