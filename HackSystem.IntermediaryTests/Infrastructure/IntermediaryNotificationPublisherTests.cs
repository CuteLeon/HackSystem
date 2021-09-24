using System.Diagnostics;
using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
using Xunit;

namespace HackSystem.Intermediary.Infrastructure.Tests
{
    public class IntermediaryNotificationPublisherTests
    {
        class TestNotification : IIntermediaryNotification { public int Value { get; set; } }

        class TestNotification_A_Handler : IIntermediaryNotificationHandler<TestNotification>
        {
            public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
                => Trace.WriteLine($"{this.GetType().Name} received notification with {notification.Value}.");
        }

        class TestNotification_B_Handler : TestNotification_A_Handler { }

        [Fact()]
        public async Task PublishTest()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging()
                .AddHackSystemIntermediary()
                .AddHackSystemNotificationHandler<TestNotification_A_Handler, TestNotification>()
                .AddHackSystemNotificationHandler<TestNotification_B_Handler, TestNotification>();
            IServiceProvider provider = services.BuildServiceProvider();
            var publisher = provider.GetRequiredService<IIntermediaryNotificationPublisher>();
            for (int index = 0; index < 5; index++)
            {
                await publisher.Publish(new TestNotification() { Value = index });
            }
        }
    }
}