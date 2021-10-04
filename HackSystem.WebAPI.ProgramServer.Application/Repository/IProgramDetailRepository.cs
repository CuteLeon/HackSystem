using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;

namespace HackSystem.WebAPI.ProgramServer.Application.Repository;

public interface IProgramDetailRepository : IRepositoryBase<ProgramDetail>
{
    Task<IEnumerable<ProgramDetail>> QueryMandatoryPrograms();
}
