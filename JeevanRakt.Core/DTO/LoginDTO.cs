using System.ComponentModel.DataAnnotations;

namespace JeevanRakt.Core.DTO
{
    /// <summary>
    /// Login Model for Login Controller
    /// </summary>
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email can't be blank")]
        //[EmailAddress(ErrorMessage = "Email should be a proper email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string? Password { get; set; }
    }
}
