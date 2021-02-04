using System;
using System.Threading.Tasks;
using HackSystem.Observer.Publisher;
using HackSystem.Observer.Subscriber;
using HackSystem.Web.ProgramSDK.ProgramComponent.ProgramMessages;
using HackSystem.Web.Scheduler.Program.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.Scheduler.Program.Disposer
{
    public class ProgramDisposer : IProgramDisposer
    {
        private readonly ILogger<IProgramDisposer> logger;
        private readonly IProcessContainer processContainer;
        private readonly IPublisher<ProgramCloseMessage> programClosePublisher;
        private readonly ISubscriber<ProgramCloseMessage> programCloseSubscriber;

        public ProgramDisposer(
            ILogger<IProgramDisposer> logger,
            IProcessContainer processContainer,
            IPublisher<ProgramCloseMessage> programClosePublisher,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.logger = logger;
            this.processContainer = processContainer;
            this.programClosePublisher = programClosePublisher;
            var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
            this.programCloseSubscriber = serviceProvider.GetService<ISubscriber<ProgramCloseMessage>>();
            this.programCloseSubscriber.HandleMessage = this.HandleProgramCloseMessage;
        }

        private async Task HandleProgramCloseMessage(ProgramCloseMessage message)
        {
            this.logger.LogInformation($"程序释放器接收到消息并广播消息，结束进程：{message.PID}");
            var process = this.processContainer.RemoveProcess(message.PID);
            process.ProgramComponent.Dispose();
            await this.programClosePublisher.Publish(message);
            GC.Collect();
        }
    }
}
