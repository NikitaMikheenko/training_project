using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Category
    {
        public int? Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "The category must start with a capital letter and contain no digits!")]
        [StringLength(50, ErrorMessage = "The name must not exceed 50 characters!")]
        public string Name { get; set; }
    }
}
