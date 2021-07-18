using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBMigration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyName",
                table: "TaskDetails",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyName",
                table: "TaskDetails");
        }
    }
}
