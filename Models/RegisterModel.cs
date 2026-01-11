using Done.Etc.ValidationAtributte;
using System.ComponentModel.DataAnnotations;

namespace Done.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Email is not valid!")]
        public string Email { get; set; } = string.Empty;
        [MinLength(8, ErrorMessage = "Password must have atleast 8 characters!"), IsUpper(ErrorMessage = "Password must have atleast one uppercase!"), SpecialCharacter(ErrorMessage = "Password must have atleast one special character!")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirmed password is required!"), Compare(nameof(Password), ErrorMessage = "Password are not the same!")]
        public string ConfirmedPassword { get; set; } = string.Empty;
    }
}
