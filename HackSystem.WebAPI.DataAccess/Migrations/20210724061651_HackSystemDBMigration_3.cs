using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackSystem.WebAPI.DataAccess.Migrations
{
    public partial class HackSystemDBMigration_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskDetails",
                columns: new[] { "TaskID", "AssemblyName", "AutomaticInterval", "Category", "ClassName", "CreateTime", "Enabled", "ExecuteDateTime", "FirstInterval", "Parameters", "ProcedureName", "Reentrant", "TaskFrequency", "TaskName" },
                values: new object[] { 1, "HackSystem.WebAPI.Tasks", new TimeSpan(0, 0, 0, 0, 0), "HackSystem", "HackSystem.WebAPI.Tasks.DatabaseBackup.IDatabaseBackupTask", new DateTime(2021, 7, 24, 14, 3, 30, 0, DateTimeKind.Unspecified), true, new DateTime(2021, 7, 24, 5, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0), "OriginDB=DATA SOURCE=.\\HackSystem.db|BackupDB=DATA SOURCE=.\\HackSystem_backup_{0}.db", "Execute", false, 3, "Database Auto Backup" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskDetails",
                keyColumn: "TaskID",
                keyValue: 1);
        }
    }
}
