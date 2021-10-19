using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.ProgramServer.Application.Repository;

public interface IUserProgramMapRepository : IRepositoryBase<UserProgramMap>
{
    Task<bool> CheckUserProgramMap(string userId, string programId);

    Task<IEnumerable<UserProgramMap>> QueryUserProgramMaps(string userId);

    Task<bool> SetUserProgramPinToDesktop(string userId, string programId, bool pinToDesktop);

    Task<bool> SetUserProgramPinToDock(string userId, string programId, bool pinToDock);

    Task<bool> SetUserProgramPinToTop(string userId, string programId, bool pinToTop);

    Task<bool> SetUserProgramRename(string userId, string programId, string rename);

    Task<bool> DeleteUserProgramMap(string userId, string programId);
}
