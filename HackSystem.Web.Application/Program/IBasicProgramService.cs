using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.Web.Application.Program;

public interface IBasicProgramService
{
    Task<IEnumerable<UserBasicProgramMapResponse>> QueryUserBasicProgramMaps();

    Task<bool> SetUserBasicProgramHide(UserBasicProgramMapRequest hideRequest);

    Task<bool> SetUserBasicProgramPinToDock(UserBasicProgramMapRequest pinToDockRequest);

    Task<bool> SetUserBasicProgramPinToTop(UserBasicProgramMapRequest pinToTopRequest);

    Task<bool> SetUserBasicProgramRename(UserBasicProgramMapRequest renameRequest);

    Task<bool> DeleteUserBasicProgramMap(string programId);
}
