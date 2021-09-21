using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.Program;

namespace HackSystem.WebAPI.Services.Programs;

public class BasicProgramDataService : RepositoryBase<BasicProgram>, IBasicProgramDataService
{
    public BasicProgramDataService(
        ILogger<BasicProgramDataService> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }

    public async Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms()
        => this.AsQueryable().Where(p => p.Integral);
}
