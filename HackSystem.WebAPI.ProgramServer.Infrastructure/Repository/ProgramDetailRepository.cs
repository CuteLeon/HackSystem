using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Programs;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;

public class ProgramDetailRepository : RepositoryBase<ProgramDetail>, IProgramDetailRepository
{
    public ProgramDetailRepository(
        ILogger<ProgramDetailRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public async Task<IEnumerable<ProgramDetail>> QueryMandatoryPrograms()
        => this.AsQueryable().Where(p => p.Mandatory);
}
