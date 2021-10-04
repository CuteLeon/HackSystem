using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;

public class UserProgramMapRepository : RepositoryBase<UserProgramMap>, IUserProgramMapRepository
{
    public UserProgramMapRepository(
        ILogger<UserProgramMapRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public async Task<bool> DeleteUserProgramMap(string userId, string programId)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        await this.RemoveAsync(map);
        return true;
    }

    public async Task<bool> CheckUserProgramMap(string userId, string programId)
    {
        return await this.AsQueryable().AnyAsync(map => map.UserId == userId && map.ProgramId == programId);
    }

    public async Task<IEnumerable<UserProgramMap>> QueryUserProgramMaps(string userId)
    {
        return await this.AsQueryable().Where(map => map.UserId == userId).ToListAsync();
    }

    public async Task<bool> SetUserProgramPinToDesktop(string userId, string programId, bool pinToDesktop)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.PinToDesktop = pinToDesktop;
        await this.UpdateAsync(map);
        return true;
    }

    public async Task<bool> SetUserProgramPinToDock(string userId, string programId, bool pinToDock)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.PinToDock = pinToDock;
        await this.UpdateAsync(map);
        return true;
    }

    public async Task<bool> SetUserProgramPinToTop(string userId, string programId, bool pinToTop)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.PinToTop = pinToTop;
        await this.UpdateAsync(map);
        return true;
    }

    public async Task<bool> SetUserProgramRename(string userId, string programId, string rename)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.Rename = rename;
        await this.UpdateAsync(map);
        return true;
    }
}
