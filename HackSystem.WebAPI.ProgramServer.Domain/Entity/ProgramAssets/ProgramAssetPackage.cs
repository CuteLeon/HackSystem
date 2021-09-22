namespace HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;

public class ProgramAssetPackage
{
    public string ProgramId { get; set; }

    public IEnumerable<ProgramAsset> ProgramAssets { get; set; }
}
