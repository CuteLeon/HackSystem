using HackSystem.Web.ProgramSchedule.Application.Disposer;
using HackSystem.Web.ProgramSchedule.Application.Container;
using HackSystem.Intermediary.Application;
using HackSystem.Web.ProgramSchedule.Domain.Notification;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.Disposer;

public class ProcessDisposer : IProcessDisposer
{
    private readonly ILogger<IProcessDisposer> logger;
    private readonly IProcessContainer processContainer;
    private readonly IIntermediaryNotificationPublisher notificationPublisher;

    public ProcessDisposer(
        ILogger<IProcessDisposer> logger,
        IProcessContainer processContainer,
        IIntermediaryNotificationPublisher notificationPublisher,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.processContainer = processContainer;
        this.notificationPublisher = notificationPublisher;
    }

    public async Task DisposeProcess(int processID)
    {
        this.logger.LogInformation($"Process closed: {processID} ID");
        _ = this.processContainer.RemoveProcess(processID);
        GC.Collect();
        await this.notificationPublisher.Publish(new ProcessDisposeNotification(processID));
    }
}
