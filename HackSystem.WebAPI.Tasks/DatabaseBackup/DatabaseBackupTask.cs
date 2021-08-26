using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Tasks.DatabaseBackup;

    public class DatabaseBackupTask : IDatabaseBackupTask
    {
        private readonly ILogger<DatabaseBackupTask> logger;

        public DatabaseBackupTask(
            ILogger<DatabaseBackupTask> logger)
        {
            this.logger = logger;
        }

        public void Execute(Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("OriginDB", out var originDB))
            {
                throw new ArgumentException("No origin db connection string.");
            }

            if (!parameters.TryGetValue("BackupDB", out var backupDB))
            {
                throw new ArgumentException("No backup db connection string.");
            }
            backupDB = string.Format(backupDB, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));

            this.logger.LogInformation($"Database backup: {originDB} => {backupDB}");
            using var originConnection = new SqliteConnection(originDB);
            using var backupConnection = new SqliteConnection(backupDB);
            originConnection.Open();
            backupConnection.Open();
            originConnection.BackupDatabase(backupConnection);
            this.logger.LogInformation($"Database backuped: {originConnection.DataSource} => {backupConnection.DataSource}");
        }
    }
