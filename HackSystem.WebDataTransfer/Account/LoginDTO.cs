using System.ComponentModel.DataAnnotations;

namespace HackSystem.WebDataTransfer.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "用户名称是必要的")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "登录密码是必要的")]
        public string Password { get; set; }
    }
}
