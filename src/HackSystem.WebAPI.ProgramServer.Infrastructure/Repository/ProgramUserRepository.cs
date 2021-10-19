using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;

public class ProgramUserRepository : RepositoryBase<ProgramUser>, IProgramUserRepository
{
    public ProgramUserRepository(
        ILogger<ProgramUserRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }
}
