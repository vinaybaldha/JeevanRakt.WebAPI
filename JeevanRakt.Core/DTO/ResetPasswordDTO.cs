using System.ComponentModel.DataAnnotations;

namespace JeevanRakt.Core.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }

    }
}
