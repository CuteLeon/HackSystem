using HackSystem.Intermediary.Extensions;
using HackSystem.Web.Application.Program.ProgramAsset;
using HackSystem.Web.ProgramSchedule.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Container;
using HackSystem.Web.ProgramSchedule.Destroyer;
using HackSystem.Web.ProgramSchedule.IDGenerator;
using HackSystem.Web.ProgramSchedule.Infrastructure.AssemblyLoader;
using HackSystem.Web.ProgramSchedule.Infrastructure.Container;
using HackSystem.Web.ProgramSchedule.Infrastructure.Destroyer;
using HackSystem.Web.ProgramSchedule.Infrastructure.IDGenerator;
using HackSystem.Web.ProgramSchedule.Infrastructure.IntermediaryHandler;
using HackSystem.Web.ProgramSchedule.Infrastructure.Launcher;
using HackSystem.Web.ProgramSchedule.Infrastructure.Scheduler;
using HackSystem.Web.ProgramSchedule.Intermediary;
using HackSystem.Web.ProgramSchedule.Launcher;
using HackSystem.Web.ProgramSchedule.Scheduler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace HackSystem.Web.ProgramSchedule.Tests
{
    public class ProgramSchedulerExtensionTests
    {
        [Fact()]
        public void AddHackSystemProgramSchedulerTest()
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddHackSystemIntermediary()
                .AddAutoMapper(typeof(ProgramSchedulerExtensionTests).Assembly)
                .AddSingleton(new Mock<IProgramAssetService>().Object)
                .AddHackSystemProgramScheduler(options =>
                {
                    options.ProgramLayerStart = 200;
                    options.TopProgramLayerStart = 850;
                });
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Assert.Same(serviceProvider.GetRequiredService<IPIDGenerator>(), serviceProvider.GetRequiredService<IPIDGenerator>());
            Assert.IsType<PIDGenerator>(serviceProvider.GetRequiredService<IPIDGenerator>());

            Assert.Same(serviceProvider.GetRequiredService<IProgramAssemblyLoader>(), serviceProvider.GetRequiredService<IProgramAssemblyLoader>());
            Assert.IsType<ProgramAssemblyLoader>(serviceProvider.GetRequiredService<IProgramAssemblyLoader>());

            Assert.Same(serviceProvider.GetRequiredService<IProcessContainer>(), serviceProvider.GetRequiredService<IProcessContainer>());
            Assert.IsType<ProcessContainer>(serviceProvider.GetRequiredService<IProcessContainer>());

            Assert.Same(serviceProvider.GetRequiredService<IProgramLauncher>(), serviceProvider.GetRequiredService<IProgramLauncher>());
            Assert.IsType<ProgramLauncher>(serviceProvider.GetRequiredService<IProgramLauncher>());

            Assert.Same(serviceProvider.GetRequiredService<IProcessDestroyer>(), serviceProvider.GetRequiredService<IProcessDestroyer>());
            Assert.IsType<ProcessDestroyer>(serviceProvider.GetRequiredService<IProcessDestroyer>());

            Assert.Same(serviceProvider.GetRequiredService<IProgramScheduler>(), serviceProvider.GetRequiredService<IProgramScheduler>());
            Assert.IsType<ProgramScheduler>(serviceProvider.GetRequiredService<IProgramScheduler>());

            Assert.Same(
                serviceProvider.GetRequiredService<IRequestHandler<ProcessDestroyCommand, ValueTuple>>(),
                serviceProvider.GetRequiredService<IRequestHandler<ProcessDestroyCommand, ValueTuple>>());
            Assert.IsType<ProcessDestroyCommandHandler>(serviceProvider.GetRequiredService<IRequestHandler<ProcessDestroyCommand, ValueTuple>>());

            Assert.Same(
                serviceProvider.GetRequiredService<IRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>>(),
                serviceProvider.GetRequiredService<IRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>>());
            Assert.IsType<ProgramLaunchRequestHandler>(serviceProvider.GetRequiredService<IRequestHandler<ProgramLaunchRequest, ProgramLaunchResponse>>());
        }
    }
}