using System.ComponentModel.DataAnnotations;

namespace VibeTogether.Authorization.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
