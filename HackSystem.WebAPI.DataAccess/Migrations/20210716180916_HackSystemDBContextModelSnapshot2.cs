using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBContextModelSnapshot2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskName = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExecuteDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AutomaticInterval = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    TaskFrequency = table.Column<int>(type: "INTEGER", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "TaskDetail_Index",
                table: "TaskDetails",
                columns: new[] { "TaskID", "ExecuteDateTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDetails");
        }
    }
}
