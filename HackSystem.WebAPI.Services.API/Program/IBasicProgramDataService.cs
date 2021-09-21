using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.Model.Program;

namespace HackSystem.WebAPI.Services.API.Program;

public interface IBasicProgramDataService : IRepositoryBase<BasicProgram>
{
    Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms();
}
