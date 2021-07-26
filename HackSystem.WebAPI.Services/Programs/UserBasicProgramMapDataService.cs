using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Map.UserMap;
using HackSystem.WebAPI.Services.API.Program;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.Programs
{
    public class UserBasicProgramMapDataService : DataServiceBase<UserBasicProgramMap>, IUserBasicProgramMapDataService
    {
        public UserBasicProgramMapDataService(
            ILogger<UserBasicProgramMapDataService> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }

        public async Task<bool> DeleteUserBasicProgramMap(string userId, string programId)
        {
            var map = await this.FindAsync(userId, programId);
            if (map == null) return false;
            await this.RemoveAsync(map);
            return true;
        }

        public async Task<IEnumerable<UserBasicProgramMap>> QueryUserBasicProgramMaps(string userId)
        {
            return await this.AsQueryable().AsNoTracking().Where(map => map.UserId == userId).ToListAsync();
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
}
