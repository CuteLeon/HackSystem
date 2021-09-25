using HackSystem.Intermediary.Application;
using HackSystem.Intermediary.Domain;
using HackSystem.Intermediary.Extensions;
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
            .AddHackSystemRequestHandler<SingletonTestRequestHandler, SingletonTestRequest, int>(ServiceLifetime.Singleton)
            .AddHackSystemRequestHandler<TestOverridedRequestHandler, TestRequest, int>()
            .AddHackSystemRequestHandler<TestRequestHandler, TestRequest, int>();
        IServiceProvider provider = services.BuildServiceProvider();
        var sender = provider.GetRequiredService<IIntermediaryRequestSender>();
        for (int index = 0; index < 10; index++)
        {
            await sender.Send(new TestRequest() { Value = index });
            await sender.Send(new SingletonTestRequest() { Value = index });
        }

        Assert.Equal(1, SingletonTestRequestHandler.HandlerInstances.Count());
        Assert.Empty(TestOverridedRequestHandler.HandlerInstances);
        Assert.Equal(10, TestRequestHandler.HandlerInstances.Count());
    }
}
