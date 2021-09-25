using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;

public class BasicProgramRepository : RepositoryBase<BasicProgram>, IBasicProgramRepository
{
    public BasicProgramRepository(
        ILogger<BasicProgramRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public async Task<IEnumerable<BasicProgram>> QueryMandatoryBasicPrograms()
        => this.AsQueryable().Where(p => p.Mandatory);
}
