using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
using Xunit;

namespace HackSystem.Intermediary.Infrastructure.Tests
{
    public class IntermediaryNotificationPublisherTests
    {
        class TestNotification : IIntermediaryNotification { public int Value { get; set; } }

        class TestNotification_Transient1_Handler : IIntermediaryNotificationHandler<TestNotification>
        {
            public static HashSet<string> HandlerInstances = new();
            public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
                => HandlerInstances.Add(this.GetHashCode().ToString("X"));
        }

        class TestNotification_Transient2_Handler : IIntermediaryNotificationHandler<TestNotification>
        {
            public static HashSet<string> HandlerInstances = new();
            public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
                => HandlerInstances.Add(this.GetHashCode().ToString("X"));
        }

        class TestNotification_Singleton1_Handler : IIntermediaryNotificationHandler<TestNotification>
        {
            public static HashSet<string> HandlerInstances = new();
            public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
                => HandlerInstances.Add(this.GetHashCode().ToString("X"));
        }

        class TestNotification_Singleton2_Handler : IIntermediaryNotificationHandler<TestNotification>
        {
            public static HashSet<string> HandlerInstances = new();
            public async Task Handle(TestNotification notification, CancellationToken cancellationToken)
                => HandlerInstances.Add(this.GetHashCode().ToString("X"));
        }

        [Fact()]
        public async Task PublishTest()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging()
                .AddHackSystemIntermediary()
                .AddHackSystemNotificationHandler<TestNotification_Transient1_Handler, TestNotification>()
                .AddHackSystemNotificationHandler<TestNotification_Transient2_Handler, TestNotification>()
                .AddHackSystemNotificationHandler<TestNotification_Singleton1_Handler, TestNotification>(ServiceLifetime.Singleton)
                .AddHackSystemNotificationHandler<TestNotification_Singleton2_Handler, TestNotification>(ServiceLifetime.Singleton);
            IServiceProvider provider = services.BuildServiceProvider();
            var publisher = provider.GetRequiredService<IIntermediaryNotificationPublisher>();
            for (int index = 0; index < 10; index++)
            {
                await publisher.Publish(new TestNotification() { Value = index });
            }

            Assert.Equal(10, TestNotification_Transient1_Handler.HandlerInstances.Count());
            Assert.Equal(10, TestNotification_Transient2_Handler.HandlerInstances.Count());
            Assert.Equal(1, TestNotification_Singleton1_Handler.HandlerInstances.Count());
            Assert.Equal(1, TestNotification_Singleton2_Handler.HandlerInstances.Count());
        }
    }
}