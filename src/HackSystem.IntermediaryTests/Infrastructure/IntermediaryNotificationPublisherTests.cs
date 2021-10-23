using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
using MediatR;
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
                .AddIntermediaryNotificationHandler<TestNotification_Transient1_Handler, TestNotification>()
                .AddIntermediaryNotificationHandler<TestNotification_Transient2_Handler, TestNotification>()
                .AddIntermediaryNotificationHandler<TestNotification_Singleton1_Handler, TestNotification>(ServiceLifetime.Singleton)
                .AddIntermediaryNotificationHandler<TestNotification_Singleton2_Handler, TestNotification>(ServiceLifetime.Singleton);
            IServiceProvider provider = services.BuildServiceProvider();
            var publisher = provider.GetRequiredService<IIntermediaryPublisher>();
            for (int index = 0; index < 10; index++)
            {
                await publisher.PublishNotification(new TestNotification() { Value = index });
            }

            Assert.Equal(10, TestNotification_Transient1_Handler.HandlerInstances.Count());
            Assert.Equal(10, TestNotification_Transient2_Handler.HandlerInstances.Count());
            Assert.Equal(1, TestNotification_Singleton1_Handler.HandlerInstances.Count());
            Assert.Equal(1, TestNotification_Singleton2_Handler.HandlerInstances.Count());
        }

        [Fact()]
        public void AddIntermediaryNotificationHandlerTest()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging()
                .AddIntermediaryNotificationHandler<TestNotification_Transient1_Handler, TestNotification>();
            IServiceProvider provider = services.BuildServiceProvider();
            Assert.NotSame(
                provider.GetRequiredService<IIntermediaryNotificationHandler<TestNotification>>(),
                provider.GetRequiredService<INotificationHandler<TestNotification>>());
            Assert.NotNull(provider.GetRequiredService<IIntermediaryNotificationHandler<TestNotification>>());
            Assert.NotNull(provider.GetRequiredService<INotificationHandler<TestNotification>>());
            Assert.IsType<TestNotification_Transient1_Handler>(provider.GetRequiredService<IIntermediaryNotificationHandler<TestNotification>>());
            Assert.IsType<TestNotification_Transient1_Handler>(provider.GetRequiredService<INotificationHandler<TestNotification>>());
        }

        [Fact()]
        public void AddIntermediaryNotificationHandlerSingletonTest()
        {
            IServiceCollection services = new ServiceCollection();
            services
                .AddLogging()
                .AddIntermediaryNotificationHandlerSingleton<IIntermediaryNotificationHandler<TestNotification>, TestNotification>(new TestNotification_Singleton1_Handler());
            IServiceProvider provider = services.BuildServiceProvider();
            Assert.Same(
                provider.GetRequiredService<IIntermediaryNotificationHandler<TestNotification>>(),
                provider.GetRequiredService<INotificationHandler<TestNotification>>());
            Assert.NotNull(provider.GetRequiredService<IIntermediaryNotificationHandler<TestNotification>>());
            Assert.NotNull(provider.GetRequiredService<INotificationHandler<TestNotification>>());
            Assert.IsType<TestNotification_Singleton1_Handler>(provider.GetRequiredService<IIntermediaryNotificationHandler<TestNotification>>());
            Assert.IsType<TestNotification_Singleton1_Handler>(provider.GetRequiredService<INotificationHandler<TestNotification>>());
        }
    }
}