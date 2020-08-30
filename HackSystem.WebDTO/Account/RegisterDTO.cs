using System.ComponentModel.DataAnnotations;

namespace HackSystem.WebDTO.Account
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name = "名称")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "密码与确认密码不一致")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}
