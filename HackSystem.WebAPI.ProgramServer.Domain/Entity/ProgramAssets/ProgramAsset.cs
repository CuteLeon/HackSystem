namespace HackSystem.WebAPI.ProgramServer.Domain.Entity.ProgramAssets;

public class ProgramAsset
{
    public string FileName { get; set; }

    public byte[] DLLBytes { get; set; }

    public byte[] PDBBytes { get; set; }
}
