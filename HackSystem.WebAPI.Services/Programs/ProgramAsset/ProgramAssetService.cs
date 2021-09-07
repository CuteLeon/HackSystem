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
            ProgramAssets = Directory.GetFiles(programAssetFolder, "*.dll", SearchOption.AllDirectories)
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
}
