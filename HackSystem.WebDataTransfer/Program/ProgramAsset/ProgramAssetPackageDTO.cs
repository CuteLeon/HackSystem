using System.Collections.Generic;

namespace HackSystem.WebDataTransfer.Program.ProgramAsset;

public class ProgramAssetPackageDTO
{
    public string ProgramId { get; set; }

    public IEnumerable<ProgramAssetDTO> ProgramAssets { get; set; }
}
