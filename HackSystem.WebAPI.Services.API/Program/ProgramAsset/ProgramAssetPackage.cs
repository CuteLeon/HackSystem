namespace HackSystem.WebAPI.Services.API.Program.ProgramAsset;

public class ProgramAssetPackage
{
    public string ProgramId { get; set; }

    public IEnumerable<ProgramAsset> ProgramAssets { get; set; }
}
