using AutoMapper;
using HackSystem.DataTransferObjects.Programs.ProgramAssets;
using HackSystem.Web.Application.Program.ProgramAsset;
using HackSystem.Web.Mappers.ProgramAsserts;
using Xunit;

namespace HackSystem.Web.ProgramSchedule.Infrastructure.AssemblyLoader.Tests
{
    public class ProgramAssemblyLoaderTests
    {
        [Fact()]
        public async Task LoadProgramAssemblyTest()
        {
            var programId = "000123";
            var programAssetService = new Mock<IProgramAssetService>();
            programAssetService.Setup(service => service.QueryProgramAssetList(It.IsAny<string>())).ReturnsAsync(new ProgramAssetPackageResponse()
            {
                ProgramId = programId,
                ProgramAssets = new[]
                {
                    new ProgramAssetResponse() { FileName = "ProgramA.SpecificAssembly.dll" },
                },
            });
            programAssetService.Setup(service => service.QueryProgramAssetPackage(It.IsAny<ProgramAssetPackageRequest>())).ReturnsAsync(new ProgramAssetPackageResponse()
            {
                ProgramId = programId,
                ProgramAssets = new[] {
                    new ProgramAssetResponse()
                    {
                        FileName = "ProgramA.SpecificAssembly.dll",
                        DLLBytes = File.ReadAllBytes(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        PDBBytes = Array.Empty<byte>()
                    },
                },
            });
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddAutoMapper(typeof(ProgramAssertMapperProfile).Assembly)
                .AddSingleton(programAssetService.Object);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var programAssemblyLoader = new ProgramAssemblyLoader(
                serviceProvider.GetRequiredService<ILogger<ProgramAssemblyLoader>>(),
                serviceProvider.GetRequiredService<IMapper>(),
                serviceProvider.GetRequiredService<IServiceScopeFactory>());
            var assemblies = await programAssemblyLoader.LoadProgramAssembly(programId);
            Assert.NotEmpty(assemblies);
        }
    }
}