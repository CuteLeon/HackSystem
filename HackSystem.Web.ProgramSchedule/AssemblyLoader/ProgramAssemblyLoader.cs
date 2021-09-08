using System.Reflection;
using System.Runtime.Loader;
using HackSystem.Web.Services.API.Program.ProgramAsset;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HackSystem.Web.ProgramSchedule.AssemblyLoader;

public class ProgramAssemblyLoader : IProgramAssemblyLoader
{
    private readonly HashSet<string> loadedAssemblies;
    private readonly ILogger<ProgramAssemblyLoader> logger;
    private readonly IProgramAssetService programAssetService;

    public ProgramAssemblyLoader(
        ILogger<ProgramAssemblyLoader> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        var serviceScope = serviceScopeFactory.CreateScope();
        this.programAssetService = serviceScope.ServiceProvider.GetRequiredService<IProgramAssetService>();
        this.loadedAssemblies = new(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName().Name));
    }

    public bool CheckAssemblyLoaded(string assemblyName)
        => this.loadedAssemblies.Contains(assemblyName);

    public async Task<IEnumerable<Assembly>> LoadProgramAssembly(string programId)
    {
        this.logger.LogInformation($"Load program asset of program {programId}...");
        var assetList = await this.programAssetService.QueryProgramAssetList(programId);
        assetList.ProgramAssets = assetList.ProgramAssets.Where(x => !this.loadedAssemblies.Contains(x.FileName)).ToList();
        this.logger.LogInformation($"Total {assetList.ProgramAssets.Count()} assets required after excludes loaded assemblies...");

        var newLoadPackage = await this.programAssetService.QueryProgramAssetPackage(assetList);
        var newLoadAssemblies = newLoadPackage.ProgramAssets
            .Select(x =>
            {
                var assembly = x.PDBBytes.Length == 0 ?
                    AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(x.DLLBytes)) :
                    AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(x.DLLBytes), new MemoryStream(x.PDBBytes));
                this.loadedAssemblies.Add(x.FileName);
                this.logger.LogInformation($"Loaded {x.FileName} assembly successfully.");
                return assembly;
            })
            .ToArray();
        this.logger.LogInformation($"Total {newLoadAssemblies.Count()} assemblies loaded successfully for program {programId}.");

        return newLoadAssemblies;
    }
}
