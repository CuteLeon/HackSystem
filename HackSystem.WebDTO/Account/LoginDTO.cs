using System.ComponentModel.DataAnnotations;

namespace HackSystem.WebDTO.Account;

public class LoginDTO
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
