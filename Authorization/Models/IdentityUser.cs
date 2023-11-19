using System.ComponentModel.DataAnnotations;

namespace VibeTogether.Authorization.Models
{
    public class IdentityUser
    {
        [Key]
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must have 3-30 symbols")]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Password must have 8-50 symbols")]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
    }
}
