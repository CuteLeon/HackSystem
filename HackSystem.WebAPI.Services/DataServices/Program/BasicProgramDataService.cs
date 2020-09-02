using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.DataServices.Program;
using Microsoft.Extensions.Logging;

namespace HackSystem.WebAPI.Services.DataServices.Program
{
    public class BasicProgramDataService : DataServiceBase<BasicProgram>, IBasicProgramDataService
    {
        public BasicProgramDataService(
            ILogger<DataServiceBase<BasicProgram>> logger,
            HackSystemDBContext hackSystemDBContext)
            : base(logger, hackSystemDBContext)
        {
        }
    }
}
