using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class UserEditModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Enter login!")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]+$", ErrorMessage = "Login must start with a letter and contains only numbers, letters or '.-_'!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Login length must be between 5 and 50 characters!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password!")]
        [RegularExpression(@"[a-zA-Z0-9]+$", ErrorMessage = "The password must contain only numbers and letters!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password length must be between 5 and 50 characters!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password!")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }

        public List<Role> Roles { get; set; }
    }
}
