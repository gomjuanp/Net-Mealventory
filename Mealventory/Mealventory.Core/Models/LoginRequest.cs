using System.ComponentModel.DataAnnotations;

namespace Mealventory.Core.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "Email is required.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
