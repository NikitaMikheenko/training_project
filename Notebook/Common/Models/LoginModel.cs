using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter login!")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]+$", ErrorMessage = "Login must start with a letter and contains only numbers, letters or '.-_'!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Login length must be between 5 and 50 characters!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password!")]
        public string Password { get; set; }
    }
}
