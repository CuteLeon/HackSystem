using HackSystem.WebAPI.Tasks.DatabaseBackup;

namespace HackSystem.WebAPI.Tasks;

public static class HackSystemTaskExtension
{
    public static IServiceCollection AddWebAPITasks(
        this IServiceCollection services)
    {
        services
            .AddTransient<IDatabaseBackupTask, DatabaseBackupTask>();

        return services;
    }
}
