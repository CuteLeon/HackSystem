using System;
using System.Threading.Tasks;
using HackSystem.Observer.Publisher;
using HackSystem.Observer.Subscriber;
using HackSystem.Web.ProgramSDK.ProgramComponent.Messages;
using HackSystem.Web.Scheduler.Program.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Disposer
{
    public class ProcessDisposer : IProcessDisposer
    {
        private readonly ILogger<IProcessDisposer> logger;
        private readonly IProcessContainer processContainer;
        private readonly IPublisher<ProcessCloseMessage> processClosePublisher;
        private readonly ISubscriber<ProcessCloseMessage> processCloseSubscriber;

        public ProcessDisposer(
            ILogger<IProcessDisposer> logger,
            IProcessContainer processContainer,
            IPublisher<ProcessCloseMessage> processClosePublisher,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.processContainer = processContainer;
            this.processClosePublisher = processClosePublisher;
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            this.processCloseSubscriber = serviceProvider.GetService<ISubscriber<ProcessCloseMessage>>();
            this.processCloseSubscriber.HandleMessage = this.HandleProcessCloseMessage;
        }

        private async Task HandleProcessCloseMessage(ProcessCloseMessage message)
        {
            this.logger.LogInformation($"Process closed: {message.PID}");
            _ = this.processContainer.RemoveProcess(message.PID);
            await this.processClosePublisher.Publish(message);
            GC.Collect();
        }
    }
}
