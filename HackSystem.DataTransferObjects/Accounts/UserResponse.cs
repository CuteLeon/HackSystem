using HackSystem.DataTransferObjects.Programs;

namespace HackSystem.DataTransferObjects.Accounts;

public class UserResponse
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public int ExperienceLevel { get; set; }

    public int ExperiencePoints { get; set; }

    public IList<UserBasicProgramMapResponse> UserProgramMaps { get; set; }
}
