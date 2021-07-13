using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebAPI.Model.Program;
using HackSystem.WebAPI.Services.API.DataServices;

namespace HackSystem.WebAPI.Services.API.Program
{
    public interface IBasicProgramDataService : IDataServiceBase<BasicProgram>
    {
        Task<IEnumerable<BasicProgram>> QueryIntegralBasicPrograms();
    }
}
