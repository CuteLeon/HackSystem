using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBMigration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_WebAPILogs_RequestURI_SourceHost_IdentityName_StartDateTime",
                table: "WebAPILogs",
                columns: new[] { "RequestURI", "SourceHost", "IdentityName", "StartDateTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebAPILogs");
        }
    }
}
