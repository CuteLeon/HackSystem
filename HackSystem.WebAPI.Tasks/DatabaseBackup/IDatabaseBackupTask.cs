using System.Collections.Generic;

namespace HackSystem.WebAPI.Tasks.DatabaseBackup
{
    public interface IDatabaseBackupTask
    {
        void Execute(Dictionary<string, string> parameters);
    }
}
