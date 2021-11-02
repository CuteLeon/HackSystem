using HackSystem.Web.ProgramPlatform.Components.ProgramComponent;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Enums;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.Launcher;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;

public class WindowLauncher : IWindowLauncher
{
    private readonly ILogger<WindowLauncher> logger;
    private readonly IIntermediaryPublisher publisher;

    public WindowLauncher(
        ILogger<WindowLauncher> logger,
        IIntermediaryPublisher publisher)
    {
        this.logger = logger;
        this.publisher = publisher;
    }

    public async Task<ProgramWindowDetail> LaunchWindow(ProcessDetail process, Type windowComponentType, ProgramWindowDetail? parentWindowDetail = null)
    {
        this.logger.LogInformation($"Launch window {windowComponentType.FullName} of process {process.ProcessId}...");
        if (!typeof(ProgramComponentBase).IsAssignableFrom(windowComponentType))
            throw new TypeLoadException($"Program entry component type must derive from {typeof(ProgramComponentBase).Name}.");

        var programWindowDetail = new ProgramWindowDetail(Guid.NewGuid().ToString(), windowComponentType, process);
        if (parentWindowDetail is not null)
            programWindowDetail.SetParentWindow(parentWindowDetail);
        process.AddWindowDetail(programWindowDetail);
        this.logger.LogInformation($"Window {programWindowDetail.Caption} ({programWindowDetail.WindowId}) of process {process.ProcessId} launched.");
        _ = await this.publisher.SendRequest(new WindowScheduleRequest(programWindowDetail, WindowChangeStates.Launch));
        return programWindowDetail;
    }
}
