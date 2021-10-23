using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
using MediatR;
using Xunit;

namespace HackSystem.Intermediary.Infrastructure.Tests;

public class IntermediaryRequestSenderTests
{
    class TestRequest : IIntermediaryRequest<int> { public int Value { get; set; } }

    class SingletonTestRequest : TestRequest { }

    class TestRequestHandler : IIntermediaryRequestHandler<TestRequest, int>
    {
        public static HashSet<string> HandlerInstances = new();
        public async Task<int> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            HandlerInstances.Add(this.GetHashCode().ToString("X"));
            return request.Value * -1;
        }
    }

    class TestOverridedRequestHandler : IIntermediaryRequestHandler<TestRequest, int>
    {
        public static HashSet<string> HandlerInstances = new();
        public async Task<int> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            HandlerInstances.Add(this.GetHashCode().ToString("X"));
            return request.Value * -1;
        }
    }

    class SingletonTestRequestHandler : IIntermediaryRequestHandler<SingletonTestRequest, int>
    {
        public static HashSet<string> HandlerInstances = new();
        public async Task<int> Handle(SingletonTestRequest request, CancellationToken cancellationToken)
        {
            HandlerInstances.Add(this.GetHashCode().ToString("X"));
            return request.Value * -1;
        }
    }

    [Fact()]
    public async Task SendTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddHackSystemIntermediary()
            .AddIntermediaryRequestHandler<SingletonTestRequestHandler, SingletonTestRequest, int>(ServiceLifetime.Singleton)
            .AddIntermediaryRequestHandler<TestOverridedRequestHandler, TestRequest, int>()
            .AddIntermediaryRequestHandler<TestRequestHandler, TestRequest, int>();
        IServiceProvider provider = services.BuildServiceProvider();
        var sender = provider.GetRequiredService<IIntermediaryPublisher>();
        for (int index = 0; index < 10; index++)
        {
            await sender.SendRequest(new TestRequest() { Value = index });
            await sender.SendRequest(new SingletonTestRequest() { Value = index });
        }

        Assert.Equal(1, SingletonTestRequestHandler.HandlerInstances.Count());
        Assert.Empty(TestOverridedRequestHandler.HandlerInstances);
        Assert.Equal(10, TestRequestHandler.HandlerInstances.Count());
    }

    [Fact()]
    public void AddIntermediaryRequestHandlerTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddIntermediaryRequestHandler<TestRequestHandler, TestRequest, int>();
        IServiceProvider provider = services.BuildServiceProvider();
        Assert.NotSame(
            provider.GetRequiredService<IIntermediaryRequestHandler<TestRequest, int>>(),
            provider.GetRequiredService<IRequestHandler<TestRequest, int>>());
        Assert.NotNull(provider.GetRequiredService<IIntermediaryRequestHandler<TestRequest, int>>());
        Assert.NotNull(provider.GetRequiredService<IRequestHandler<TestRequest, int>>());
        Assert.IsType<TestRequestHandler>(provider.GetRequiredService<IIntermediaryRequestHandler<TestRequest, int>>());
        Assert.IsType<TestRequestHandler>(provider.GetRequiredService<IRequestHandler<TestRequest, int>>());
    }

    [Fact()]
    public void AddIntermediaryRequestHandlerSingletonTest()
    {
        IServiceCollection services = new ServiceCollection();
        services
            .AddLogging()
            .AddIntermediaryRequestHandlerSingleton<IIntermediaryRequestHandler<SingletonTestRequest, int>, SingletonTestRequest, int>(new SingletonTestRequestHandler());
        IServiceProvider provider = services.BuildServiceProvider();
        Assert.Same(
            provider.GetRequiredService<IIntermediaryRequestHandler<SingletonTestRequest, int>>(),
            provider.GetRequiredService<IRequestHandler<SingletonTestRequest, int>>());
        Assert.NotNull(provider.GetRequiredService<IIntermediaryRequestHandler<SingletonTestRequest, int>>());
        Assert.NotNull(provider.GetRequiredService<IRequestHandler<SingletonTestRequest, int>>());
        Assert.IsType<SingletonTestRequestHandler>(provider.GetRequiredService<IIntermediaryRequestHandler<SingletonTestRequest, int>>());
        Assert.IsType<SingletonTestRequestHandler>(provider.GetRequiredService<IRequestHandler<SingletonTestRequest, int>>());
    }
}
