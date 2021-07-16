using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.DataAccess.API.DataServices;
using HackSystem.WebAPI.Model.Program;

namespace HackSystem.WebAPI.Services.API.Program
{
    public interface IBasicProgramDataService : IDataServiceBase<BasicProgram>
    {
        Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms();
    }
}
