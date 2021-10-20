using HackSystem.Web.ProgramPlatform.Components.ProgramComponent;
using HackSystem.Web.ProgramSchedule.Abstractions.Enums;
using HackSystem.Web.ProgramSchedule.Entity;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.Launcher;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;

public class WindowLauncher : IWindowLauncher
{
    private readonly ILogger<WindowLauncher> logger;
    private readonly IIntermediaryRequestSender requestSender;
    private readonly IIntermediaryEventPublisher eventPublisher;

    public WindowLauncher(
        ILogger<WindowLauncher> logger,
        IIntermediaryRequestSender requestSender,
        IIntermediaryEventPublisher eventPublisher)
    {
        this.logger = logger;
        this.requestSender = requestSender;
        this.eventPublisher = eventPublisher;
    }

    public async Task<ProgramWindowDetail> LaunchWindow(ProcessDetail process, Type windowComponentType, string caption)
    {
        this.logger.LogInformation($"Launch window {windowComponentType.FullName} of process {process.ProcessId}...");
        if (!typeof(ProgramComponentBase).IsAssignableFrom(windowComponentType))
            throw new TypeLoadException($"Program entry component type must derive from {typeof(ProgramComponentBase).Name}.");

        var programWindowDetail = new ProgramWindowDetail(Guid.NewGuid().ToString(), windowComponentType, process) { Caption = caption };
        process.AddWindowDetail(programWindowDetail);
        this.logger.LogInformation($"Window {programWindowDetail.Caption} ({programWindowDetail.WindowId}) of process {process.ProcessId} launched.");
        _ = await this.requestSender.Send(new WindowScheduleRequest(programWindowDetail, WindowChangeStates.Launch));
        await this.eventPublisher.Publish(new WindowChangeEvent(WindowChangeStates.Launch, programWindowDetail));
        return programWindowDetail;
    }
}
