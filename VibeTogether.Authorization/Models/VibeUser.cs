using Microsoft.AspNetCore.Identity;

namespace VibeTogether.Authorization.Models
{
    public class VibeUser : IdentityUser
    {
        public string? Avatar { get; set; }
    }
}
