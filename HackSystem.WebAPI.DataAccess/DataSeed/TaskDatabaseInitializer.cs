using System;
using HackSystem.WebAPI.Model.Task;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.DataAccess.DataSeed
{
    public static class TaskDatabaseInitializer
    {
        public static ModelBuilder InitializeTaskData(this ModelBuilder builder)
        {
            builder.Entity<TaskDetail>().HasData(new[]
            {
                new TaskDetail()
                {
                    TaskID = 1,
                    Enabled = true,
                    TaskFrequency = TaskFrequency.Daily,
                    ExecuteDateTime=new DateTime(2021,07,24,5,0,0),
                    TaskName= "Database Auto Backup",
                    AssemblyName = "HackSystem.WebAPI.Tasks",
                    ClassName = "HackSystem.WebAPI.Tasks.DatabaseBackup.IDatabaseBackupTask",
                    ProcedureName="Execute",
                    Parameters= "OriginDB=DATA SOURCE=.\\HackSystem.db|BackupDB=DATA SOURCE=.\\HackSystem_backup_{0}.db",
                    FirstInterval=TimeSpan.Zero,
                    AutomaticInterval = TimeSpan.Zero,
                    Reentrant=false,
                    CreateTime= new DateTime(2021,07,24,14,3,30),
                    Category = "HackSystem"
                },
            });

            return builder;
        }
    }
}
