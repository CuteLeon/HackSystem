using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.Web.Application.Program;

public interface IProgramDetailService
{
    Task<IEnumerable<UserProgramMapResponse>> QueryUserProgramMaps();

    Task<bool> SetUserProgramHide(UserProgramMapRequest hideRequest);

    Task<bool> SetUserProgramPinToDock(UserProgramMapRequest pinToDockRequest);

    Task<bool> SetUserProgramPinToTop(UserProgramMapRequest pinToTopRequest);

    Task<bool> SetUserProgramRename(UserProgramMapRequest renameRequest);

    Task<bool> DeleteUserProgramMap(string programId);
}
