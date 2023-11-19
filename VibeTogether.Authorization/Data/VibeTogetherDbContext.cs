using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VibeTogether.Authorization.Models;

namespace VibeTogether.Authorization.Data
{
    public class VibeTogetherDbContext : IdentityDbContext<VibeUser>
    {
        public VibeTogetherDbContext(DbContextOptions<VibeTogetherDbContext> options) : base(options)
        {

        }
    }
}
