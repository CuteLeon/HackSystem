namespace HackSystem.Web.TaskSchedule;

public static class Launcher
{
    public static Type Launch(LaunchParameter parameter)
    {
        return typeof(TaskSchedulerComponent);
    }
}
