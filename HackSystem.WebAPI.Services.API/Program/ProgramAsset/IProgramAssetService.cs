namespace HackSystem.WebAPI.Services.API.Program.ProgramAsset
{
    public interface IProgramAssetService
    {
        Task<ProgramAssetPackage> QueryProgramAssetPackage(string programId);
    }
}
