using System.ComponentModel.DataAnnotations;

namespace Done.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Email is not valid!")]
        public string Email { get; set; } = string.Empty;
        [MinLength(8, ErrorMessage = "Password must have atleast 8 characters!")]
        public string Password { get; set; } = string.Empty;
    }
}
