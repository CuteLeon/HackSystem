using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.Web.Application.Program;

public interface IProgramDetailService
{
    Task<IEnumerable<UserProgramMapResponse>> QueryUserProgramMaps();

    Task<bool> UpdateUserProgram(UserProgramMapRequest request);

    Task<bool> DeleteUserProgramMap(string programId);
}
