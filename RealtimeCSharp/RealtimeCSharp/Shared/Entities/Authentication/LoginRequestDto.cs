using System.ComponentModel.DataAnnotations;

namespace RealtimeCSharp.Shared.Entities.Authentication
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(256, ErrorMessage = "Email cannot be longer than 256 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Needs to be at least six characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
