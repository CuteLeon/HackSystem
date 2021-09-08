using HackSystem.WebAPI.Services.API.Program.ProgramAsset;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProgramAssetModel = HackSystem.WebAPI.Services.API.Program.ProgramAsset.ProgramAsset;

namespace HackSystem.WebAPI.Services.Programs.ProgramAsset;

public class ProgramAssetService : IProgramAssetService
{
    private readonly ILogger<ProgramAssetService> logger;
    private readonly ProgramAssetOptions programAssetOptions;

    public ProgramAssetService(
        ILogger<ProgramAssetService> logger,
        IOptions<ProgramAssetOptions> options)
    {
        this.logger = logger;
        this.programAssetOptions = options.Value;
    }

    public async Task<ProgramAssetPackage> QueryProgramAssetList(string programId)
    {
        var programAssetFolder = Path.Combine(this.programAssetOptions.FolderPath, programId);
        if (!Directory.Exists(programAssetFolder))
        {
            throw new DirectoryNotFoundException(programAssetFolder);
        }

        var package = new ProgramAssetPackage
        {
            ProgramId = programId,
            ProgramAssets = Directory
                .GetFiles(programAssetFolder, "*.dll", SearchOption.TopDirectoryOnly)
                .Select(dll => new ProgramAssetModel { FileName = Path.GetFileNameWithoutExtension(dll) })
                .ToArray()
        };
        return package;
    }

    public async Task<ProgramAssetPackage> QueryProgramAssetPackage(string programId)
    {
        var programAssetFolder = Path.Combine(this.programAssetOptions.FolderPath, programId);
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
                    var asset = new ProgramAssetModel
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
        var programAssetFolder = Path.Combine(this.programAssetOptions.FolderPath, package.ProgramId);
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
