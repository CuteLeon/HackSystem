using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.Program;
using HackSystem.WebAPI.Services.DataServices;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.Program
{
    public class BasicProgramDataService : DataServiceBase<BasicProgram>, IBasicProgramDataService
    {
        public BasicProgramDataService(
            ILogger<DataServiceBase<BasicProgram>> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }

        public async Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms()
            => this.AsQueryable().Where(p => p.Integral);
    }
}
