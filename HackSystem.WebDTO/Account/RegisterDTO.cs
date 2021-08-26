using System.ComponentModel.DataAnnotations;

namespace HackSystem.WebDTO.Account;

    public class RegisterDTO
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "EMail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm password does not match with Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
