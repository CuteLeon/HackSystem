using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.DataAccess.DataServices;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.Program;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.Programs
{
    public class BasicProgramDataService : DataServiceBase<BasicProgram>, IBasicProgramDataService
    {
        public BasicProgramDataService(
            ILogger<BasicProgramDataService> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }

        public async Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms()
            => this.AsQueryable().AsNoTracking().Where(p => p.Integral);
    }
}
