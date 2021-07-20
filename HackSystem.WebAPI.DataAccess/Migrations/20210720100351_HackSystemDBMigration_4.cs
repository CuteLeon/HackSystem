using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBMigration_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConnectionID",
                table: "MockRouteLogDetails",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionID",
                table: "MockRouteLogDetails");
        }
    }
}
