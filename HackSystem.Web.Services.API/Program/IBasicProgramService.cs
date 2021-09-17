using HackSystem.WebDataTransfer.Program;

namespace HackSystem.Web.Services.API.Program;

public interface IBasicProgramService
{
    Task<IEnumerable<UserBasicProgramMapDTO>> QueryUserBasicProgramMaps();

    Task<bool> SetUserBasicProgramHide(SetUserProgramHideDTO hideDTO);

    Task<bool> SetUserBasicProgramPinToDock(SetUserBasicProgramPinToDockDTO pinToDockDTO);

    Task<bool> SetUserBasicProgramPinToTop(SetUserBasicProgramPinToTopDTO pinToTopDTO);

    Task<bool> SetUserBasicProgramRename(SetUserBasicProgramRenameDTO renameDTO);

    Task<bool> DeleteUserBasicProgramMap(string programId);
}
