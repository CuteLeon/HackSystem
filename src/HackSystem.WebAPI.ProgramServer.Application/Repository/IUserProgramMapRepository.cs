using HackSystem.WebAPI.Application.Repository.Abstractions;
using HackSystem.WebAPI.ProgramServer.Domain.Entity.Maps;

namespace HackSystem.WebAPI.ProgramServer.Application.Repository;

public interface IUserProgramMapRepository : IRepositoryBase<UserProgramMap>
{
    Task<bool> CheckUserProgramMap(string userId, string programId);

    Task<IEnumerable<UserProgramMap>> QueryUserProgramMaps(string userId);

    Task<bool> DeleteUserProgramMap(string userId, string programId);
}
