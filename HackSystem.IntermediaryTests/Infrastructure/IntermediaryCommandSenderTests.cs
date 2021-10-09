using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
using MediatR;
using Xunit;

namespace HackSystem.Intermediary.Infrastructure.Tests;

public class IntermediaryCommandSenderTests
{
    class TestCommand : IIntermediaryCommand { public int Value { get; set; } }

    class SingletonTestCommand : TestCommand { }

    class TestCommandHandler : IIntermediaryCommandHandler<TestCommand>
    {
        public static HashSet<string> HandlerInstances = new();
        public async Task<ValueTuple> Handle(TestCommand command, CancellationToken cancellationToken)
        {
            HandlerInstances.Add(this.GetHashCode().ToString("X"));
            return ValueTuple.Create();
        }
    }

    class TestOverridedCommandHandler : IIntermediaryCommandHandler<TestCommand>
    {
        public static HashSet<string> HandlerInstances = new();
        public async Task<ValueTuple> Handle(TestCommand command, CancellationToken cancellationToken)
        {
            HandlerInstances.Add(this.GetHashCode().ToString("X"));
            return ValueTuple.Create();
        }
    }

    class SingletonTestCommandHandler : IIntermediaryCommandHandler<SingletonTestCommand>
    {
        public static HashSet<string> HandlerInstances = new();
        public async Task<ValueTuple> Handle(SingletonTestCommand command, CancellationToken cancellationToken)
        {
            HandlerInstances.Add(this.GetHashCode().ToString("X"));
            return ValueTuple.Create();
        }
    }

    [Fact()]
    public async Task SendTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryCommandHandler<SingletonTestCommandHandler, SingletonTestCommand>(ServiceLifetime.Singleton)
            .AddIntermediaryCommandHandler<TestOverridedCommandHandler, TestCommand>()
            .AddIntermediaryCommandHandler<TestCommandHandler, TestCommand>();
        IServiceProvider provider = services.BuildServiceProvider();
        var sender = provider.GetRequiredService<IIntermediaryCommandSender>();
        for (int index = 0; index < 10; index++)
        {
            await sender.Send(new TestCommand() { Value = index });
            await sender.Send(new SingletonTestCommand() { Value = index });
        }

        Assert.Equal(1, SingletonTestCommandHandler.HandlerInstances.Count());
        Assert.Empty(TestOverridedCommandHandler.HandlerInstances);
        Assert.Equal(10, TestCommandHandler.HandlerInstances.Count());
    }

    [Fact()]
    public void AddIntermediaryCommandHandlerTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddIntermediaryCommandHandler<TestCommandHandler, TestCommand>();
        IServiceProvider provider = services.BuildServiceProvider();
        Assert.NotSame(
            provider.GetRequiredService<IIntermediaryCommandHandler<TestCommand>>(),
            provider.GetRequiredService<IRequestHandler<TestCommand, ValueTuple>>());
        Assert.NotNull(provider.GetRequiredService<IIntermediaryCommandHandler<TestCommand>>());
        Assert.NotNull(provider.GetRequiredService<IRequestHandler<TestCommand, ValueTuple>>());
        Assert.IsType<TestCommandHandler>(provider.GetRequiredService<IIntermediaryCommandHandler<TestCommand>>());
        Assert.IsType<TestCommandHandler>(provider.GetRequiredService<IRequestHandler<TestCommand, ValueTuple>>());
    }

    [Fact()]
    public void AddIntermediaryCommandHandlerSingletonTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddIntermediaryCommandHandlerSingleton(new SingletonTestCommandHandler());
        IServiceProvider provider = services.BuildServiceProvider();
        Assert.Same(
            provider.GetRequiredService<IIntermediaryCommandHandler<SingletonTestCommand>>(),
            provider.GetRequiredService<IRequestHandler<SingletonTestCommand, ValueTuple>>());
        Assert.NotNull(provider.GetRequiredService<IIntermediaryCommandHandler<SingletonTestCommand>>());
        Assert.NotNull(provider.GetRequiredService<IRequestHandler<SingletonTestCommand, ValueTuple>>());
        Assert.IsType<SingletonTestCommandHandler>(provider.GetRequiredService<IIntermediaryCommandHandler<SingletonTestCommand>>());
        Assert.IsType<SingletonTestCommandHandler>(provider.GetRequiredService<IRequestHandler<SingletonTestCommand, ValueTuple>>());
    }
}
