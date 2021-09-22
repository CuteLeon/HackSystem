using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Application.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;
using Microsoft.EntityFrameworkCore;

namespace HackSystem.WebAPI.ProgramServer.Infrastructure.Repository;

public class UserBasicProgramMapRepository : RepositoryBase<UserBasicProgramMap>, IUserBasicProgramMapRepository
{
    public UserBasicProgramMapRepository(
        ILogger<UserBasicProgramMapRepository> logger,
        DbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public async Task<bool> DeleteUserBasicProgramMap(string userId, string programId)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        await this.RemoveAsync(map);
        return true;
    }

    public async Task<bool> CheckUserBasicProgramMap(string userId, string programId)
    {
        return await this.AsQueryable().AnyAsync(map => map.UserId == userId && map.ProgramId == programId);
    }

    public async Task<IEnumerable<UserBasicProgramMap>> QueryUserBasicProgramMaps(string userId)
    {
        return await this.AsQueryable().Where(map => map.UserId == userId).ToListAsync();
    }

    public async Task<bool> SetUserBasicProgramHide(string userId, string programId, bool hide)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.Hide = hide;
        await this.UpdateAsync(map);
        return true;
    }

    public async Task<bool> SetUserBasicProgramPinToDock(string userId, string programId, bool pinToDock)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.PinToDock = pinToDock;
        await this.UpdateAsync(map);
        return true;
    }

    public async Task<bool> SetUserBasicProgramPinToTop(string userId, string programId, bool pinToTop)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.PinToTop = pinToTop;
        await this.UpdateAsync(map);
        return true;
    }

    public async Task<bool> SetUserBasicProgramRename(string userId, string programId, string rename)
    {
        var map = await this.FindAsync(userId, programId);
        if (map == null) return false;
        map.Rename = rename;
        await this.UpdateAsync(map);
        return true;
    }
}
