using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBContextModelSnapshot3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserBasicProgramMap_Index",
                table: "UserBasicProgramMaps");

            migrationBuilder.DropIndex(
                name: "TaskDetail_Index",
                table: "TaskDetails");

            migrationBuilder.RenameIndex(
                name: "UserBasicProgramMap_UserId_Index",
                table: "UserBasicProgramMaps",
                newName: "IX_UserBasicProgramMaps_UserId");

            migrationBuilder.RenameIndex(
                name: "BasicProgram_Index",
                table: "BasicPrograms",
                newName: "IX_BasicPrograms_Id_Name");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FirstInterval",
                table: "TaskDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "Reentrant",
                table: "TaskDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserBasicProgramMaps_UserId_ProgramId",
                table: "UserBasicProgramMaps",
                columns: new[] { "UserId", "ProgramId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskName",
                table: "TaskDetails",
                column: "TaskName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_TaskName_ExecuteDateTime",
                table: "TaskDetails",
                columns: new[] { "TaskName", "ExecuteDateTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserBasicProgramMaps_UserId_ProgramId",
                table: "UserBasicProgramMaps");

            migrationBuilder.DropIndex(
                name: "IX_TaskDetails_TaskName",
                table: "TaskDetails");

            migrationBuilder.DropIndex(
                name: "IX_TaskDetails_TaskName_ExecuteDateTime",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "FirstInterval",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "Reentrant",
                table: "TaskDetails");

            migrationBuilder.RenameIndex(
                name: "IX_UserBasicProgramMaps_UserId",
                table: "UserBasicProgramMaps",
                newName: "UserBasicProgramMap_UserId_Index");

            migrationBuilder.RenameIndex(
                name: "IX_BasicPrograms_Id_Name",
                table: "BasicPrograms",
                newName: "BasicProgram_Index");

            migrationBuilder.CreateIndex(
                name: "UserBasicProgramMap_Index",
                table: "UserBasicProgramMaps",
                columns: new[] { "UserId", "ProgramId" });

            migrationBuilder.CreateIndex(
                name: "TaskDetail_Index",
                table: "TaskDetails",
                columns: new[] { "TaskID", "ExecuteDateTime" });
        }
    }
}
