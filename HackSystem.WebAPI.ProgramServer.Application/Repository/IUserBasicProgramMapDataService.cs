using HackSystem.WebAPI.DataAccess.API.Repository;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.ProgramServer.Application.Repository;

public interface IUserBasicProgramMapDataService : IRepositoryBase<UserBasicProgramMap>
{
    Task<bool> CheckUserBasicProgramMap(string userId, string programId);

    Task<IEnumerable<UserBasicProgramMap>> QueryUserBasicProgramMaps(string userId);

    Task<bool> SetUserBasicProgramHide(string userId, string programId, bool hide);

    Task<bool> SetUserBasicProgramPinToDock(string userId, string programId, bool pinToDock);

    Task<bool> SetUserBasicProgramPinToTop(string userId, string programId, bool pinToTop);

    Task<bool> SetUserBasicProgramRename(string userId, string programId, string rename);

    Task<bool> DeleteUserBasicProgramMap(string userId, string programId);
}
