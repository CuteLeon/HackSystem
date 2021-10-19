using HackSystem.WebAPI.ProgramServer.Application.Repository.ProgramAssets;
using HackSystem.WebAPI.ProgramServer.Domain.Configurations;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository.ProgramAssets;

public class ProgramAssetService : IProgramAssetService
{
    private readonly ILogger<ProgramAssetService> logger;
    private readonly IOptionsSnapshot<ProgramAssetOptions> options;

    public ProgramAssetService(
        ILogger<ProgramAssetService> logger,
        IOptionsSnapshot<ProgramAssetOptions> options)
    {
        this.logger = logger;
        this.options = options;
    }

    public async Task<ProgramAssetPackage> QueryProgramAssetList(string programId)
    {
        var programAssetFolder = Path.Combine(this.options.Value.FolderPath, programId);
        if (!Directory.Exists(programAssetFolder))
        {
            throw new DirectoryNotFoundException(programAssetFolder);
        }

        var package = new ProgramAssetPackage
        {
            ProgramId = programId,
            ProgramAssets = Directory
                .GetFiles(programAssetFolder, "*.dll", SearchOption.TopDirectoryOnly)
                .Select(dll => new ProgramAsset { FileName = Path.GetFileNameWithoutExtension(dll) })
                .ToArray()
        };
        return package;
    }

    public async Task<ProgramAssetPackage> QueryProgramAssetPackage(string programId)
    {
        var programAssetFolder = Path.Combine(this.options.Value.FolderPath, programId);
        if (!Directory.Exists(programAssetFolder))
        {
            throw new DirectoryNotFoundException(programAssetFolder);
        }

        const int extensionLength = 4;
        var package = new ProgramAssetPackage
        {
            ProgramId = programId,
            ProgramAssets = Directory.GetFiles(programAssetFolder, "*.dll", SearchOption.TopDirectoryOnly)
                .Select(dll =>
                {
                    var pdb = string.Concat(dll.AsSpan(0, dll.Length - extensionLength), ".pdb");
                    var asset = new ProgramAsset
                    {
                        FileName = Path.GetFileNameWithoutExtension(dll),
                        DLLBytes = File.ReadAllBytes(dll),
                        PDBBytes = File.Exists(pdb) ? File.ReadAllBytes(pdb) : default,
                    };
                    return asset;
                })
                .ToArray()
        };
        return package;
    }

    public async Task<ProgramAssetPackage> QueryProgramAssetPackage(ProgramAssetPackage package)
    {
        var programAssetFolder = Path.Combine(this.options.Value.FolderPath, package.ProgramId);
        if (!Directory.Exists(programAssetFolder))
        {
            throw new DirectoryNotFoundException(programAssetFolder);
        }

        foreach (var programAsset in package.ProgramAssets
            .Where(x => !string.IsNullOrEmpty(x.FileName)))
        {
            var dllPath = Path.Combine(programAssetFolder, programAsset.FileName + ".dll");
            var pdbPath = Path.Combine(programAssetFolder, programAsset.FileName + ".pdb");
            programAsset.DLLBytes = File.Exists(dllPath) ? File.ReadAllBytes(dllPath) : default;
            programAsset.PDBBytes = File.Exists(pdbPath) ? File.ReadAllBytes(pdbPath) : default;
        }
        return package;
    }
}
