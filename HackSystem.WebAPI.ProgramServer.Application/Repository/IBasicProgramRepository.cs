using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.ProgramServer.Application.Repository;

public interface IBasicProgramRepository : IRepositoryBase<BasicProgram>
{
    Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms();
}
