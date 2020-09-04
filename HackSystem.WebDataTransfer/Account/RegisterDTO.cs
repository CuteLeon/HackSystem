using System.ComponentModel.DataAnnotations;

namespace HackSystem.WebDataTransfer.Account
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "用户名称是必要的")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="用户邮箱需要符合邮箱格式")]
        [Required(ErrorMessage = "用户邮箱是必要的")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "登录密码是必要的")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "确认密码是必要的")]
        [Compare(nameof(Password), ErrorMessage = "密码与确认密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
