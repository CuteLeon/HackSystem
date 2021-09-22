using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.Repository;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;

public class BasicProgramRepository : RepositoryBase<BasicProgram>, IBasicProgramRepository
{
    public BasicProgramRepository(
        ILogger<BasicProgramRepository> logger,
        HackSystemDBContext hackSystemDBContext)
        : base(logger, hackSystemDBContext)
    {
    }

    public async Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms()
        => this.AsQueryable().Where(p => p.Integral);
}
