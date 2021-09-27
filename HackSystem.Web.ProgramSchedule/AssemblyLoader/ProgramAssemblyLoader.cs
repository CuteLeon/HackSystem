using System.Reflection;
using System.Runtime.Loader;
using AutoMapper;
using HackSystem.DataTransferObjects.Programs.ProgramAssets;
using HackSystem.Web.Application.Program.ProgramAsset;

namespace HackSystem.Web.ProgramSchedule.AssemblyLoader;

public class ProgramAssemblyLoader : IProgramAssemblyLoader
{
    private readonly HashSet<string> loadedAssemblies;
    private readonly ILogger<ProgramAssemblyLoader> logger;
    private readonly IMapper mapper;
    private readonly IProgramAssetService programAssetService;

    public ProgramAssemblyLoader(
        ILogger<ProgramAssemblyLoader> logger,
        IMapper mapper,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.mapper = mapper;
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
        var assetListRequest = this.mapper.Map<ProgramAssetPackageResponse, ProgramAssetPackageRequest>(assetList);
        var newLoadPackage = await this.programAssetService.QueryProgramAssetPackage(assetListRequest);
        var newLoadAssemblies = newLoadPackage.ProgramAssets
            .Select(x =>
            {
                // Secondary AppDomain not supported on this platform, so have to load assemblies to current AppDomain;
                var assemblyLoadContext = AssemblyLoadContext.Default;
                var assembly = assemblyLoadContext.LoadFromStream(
                    new MemoryStream(x.DLLBytes),
                    x.PDBBytes.Length == 0 ? null : new MemoryStream(x.PDBBytes));
                this.loadedAssemblies.Add(x.FileName);
                this.logger.LogInformation($"Loaded {x.FileName} assembly successfully.");
                return assembly;
            })
            .ToArray();
        this.logger.LogInformation($"Total {newLoadAssemblies.Count()} assemblies loaded successfully for program {programId}.");

        return newLoadAssemblies;
    }
}
