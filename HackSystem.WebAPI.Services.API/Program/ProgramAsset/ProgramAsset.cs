namespace HackSystem.WebAPI.Services.API.Program.ProgramAsset;

public class ProgramAsset
{
    public string FileName { get; set; }

    public byte[] DLLBytes { get; set; }

    public byte[] PDBBytes { get; set; }
}
