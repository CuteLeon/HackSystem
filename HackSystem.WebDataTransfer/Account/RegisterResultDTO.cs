namespace HackSystem.WebDataTransfer.Account;

public class RegisterResultDTO
{
    public bool Successful { get; set; }

    public IEnumerable<string> Errors { get; set; }
}
