using HackSystem.Observer.Subscriber;
using HackSystem.Web.ProgramSDK.ProgramComponent.Messages;
using HackSystem.Web.ProgramSchedule.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace HackSystem.Web.ProgramSchedule.Disposer;

public class ProcessDisposer : IProcessDisposer
{
    private readonly ILogger<IProcessDisposer> logger;
    private readonly IProcessContainer processContainer;
    private readonly ISubscriber<ProcessCloseMessage> processCloseSubscriber;

    public ProcessDisposer(
        ILogger<IProcessDisposer> logger,
        IProcessContainer processContainer,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.processContainer = processContainer;
        var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
        this.processCloseSubscriber = serviceProvider.GetService<ISubscriber<ProcessCloseMessage>>();
        this.processCloseSubscriber.HandleMessage = this.HandleProcessCloseMessage;
    }

    private async Task HandleProcessCloseMessage(ProcessCloseMessage message)
    {
        this.logger.LogInformation($"Process closed: {message.PID} ID");
        _ = this.processContainer.RemoveProcess(message.PID);
        GC.Collect();
        await Task.CompletedTask;
    }
}
