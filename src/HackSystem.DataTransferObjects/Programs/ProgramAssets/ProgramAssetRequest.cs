namespace HackSystem.DataTransferObjects.Programs.ProgramAssets;

public class ProgramAssetRequest
{
    public string FileName { get; set; }

    public byte[] DLLBytes { get; set; }

    public byte[] PDBBytes { get; set; }
}
