namespace HackSystem.DataTransferObjects.Accounts;

public class LoginResponse
{
    public bool Successful { get; set; }

    public string Error { get; set; }

    public string Token { get; set; }
}
