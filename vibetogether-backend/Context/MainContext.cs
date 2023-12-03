using Microsoft.EntityFrameworkCore;
using vibetogether_backend.Models;

namespace vibetogether_backend.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }

        public DbSet<Rooms> Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
