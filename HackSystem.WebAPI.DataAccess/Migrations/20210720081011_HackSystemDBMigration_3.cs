using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBMigration_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ForwardDelayDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardStatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardResponseBodyTemplate = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardMockType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockRouteDetails", x => x.RouteID);
                });

            migrationBuilder.CreateTable(
                name: "MockRouteLogDetails",
                columns: table => new
                {
                    RouteLogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteID = table.Column<int>(type: "INTEGER", nullable: false),
                    MockURI = table.Column<string>(type: "TEXT", nullable: true),
                    MockMethod = table.Column<string>(type: "TEXT", nullable: true),
                    MockSourceHost = table.Column<string>(type: "TEXT", nullable: true),
                    StatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestBody = table.Column<string>(type: "TEXT", nullable: true),
                    ResponseBody = table.Column<string>(type: "TEXT", nullable: true),
                    MockType = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ForwardStatusCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ForwardRequestBody = table.Column<string>(type: "TEXT", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_Enabled",
                table: "TaskDetails",
                column: "Enabled");

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
                name: "IX_MockRouteLogDetails_MockURI_MockMethod_MockSourceHost_MockType",
                table: "MockRouteLogDetails",
                columns: new[] { "MockURI", "MockMethod", "MockSourceHost", "MockType" });

            migrationBuilder.CreateIndex(
                name: "IX_MockRouteLogDetails_RouteID",
                table: "MockRouteLogDetails",
                column: "RouteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MockRouteLogDetails");

            migrationBuilder.DropTable(
                name: "MockRouteDetails");

            migrationBuilder.DropIndex(
                name: "IX_TaskDetails_Enabled",
                table: "TaskDetails");
        }
    }
}
