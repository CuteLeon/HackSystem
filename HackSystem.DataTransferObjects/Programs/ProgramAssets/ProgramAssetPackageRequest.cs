namespace HackSystem.DataTransferObjects.Programs.ProgramAssets;

public class ProgramAssetPackageRequest
{
    public string ProgramId { get; set; }

    public IEnumerable<ProgramAssetRequest> ProgramAssets { get; set; }
}
