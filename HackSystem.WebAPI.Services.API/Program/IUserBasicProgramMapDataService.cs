using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Map.UserMap;

namespace HackSystem.WebAPI.Services.API.Program;

    public interface IUserBasicProgramMapDataService : IDataServiceBase<UserBasicProgramMap>
    {
        Task<IEnumerable<UserBasicProgramMap>> QueryUserBasicProgramMaps(string userId);

        Task<bool> SetUserBasicProgramHide(string userId, string programId, bool hide);

        Task<bool> SetUserBasicProgramPinToDock(string userId, string programId, bool pinToDock);

        Task<bool> SetUserBasicProgramPinToTop(string userId, string programId, bool pinToTop);

        Task<bool> SetUserBasicProgramRename(string userId, string programId, string rename);

        Task<bool> DeleteUserBasicProgramMap(string userId, string programId);
    }
