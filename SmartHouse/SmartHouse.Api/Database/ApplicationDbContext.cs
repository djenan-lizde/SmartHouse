using Microsoft.EntityFrameworkCore;
using SmartHouse.Models.Models;

namespace SmartHouse.Api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Models.Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
