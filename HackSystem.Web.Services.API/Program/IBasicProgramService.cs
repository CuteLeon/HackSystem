using System.Collections.Generic;
using System.Threading.Tasks;
using HackSystem.WebDataTransfer.Program;

namespace HackSystem.Web.Services.API.Program
{
    public interface IBasicProgramService
    {
        Task<IEnumerable<QueryBasicProgramDTO>> GetAll();

        Task<QueryBasicProgramDTO> Get(string programId);

        Task<QueryBasicProgramDTO> Create(CreateBasicProgramDTO basicProgram);

        Task<QueryBasicProgramDTO> Update(UpdateBasicProgramDTO basicProgram);

        Task<QueryBasicProgramDTO> Delete(string programId);
    }
}
