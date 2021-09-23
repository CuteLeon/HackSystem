namespace HackSystem.DataTransferObjects.Programs.ProgramAssets;

public class ProgramAssetPackageResponse
{
    public string ProgramId { get; set; }

    public IEnumerable<ProgramAssetResponse> ProgramAssets { get; set; }
}
