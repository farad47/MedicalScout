using MCPServer.Entities;
using MCPServer.Model;
using Microsoft.EntityFrameworkCore;

namespace MCPServer.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<BloodTest> BloodTests => Set<BloodTest>();
        public DbSet<Indication> Indications => Set<Indication>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodTest>(e =>
            {
                e.HasIndex(t => t.Code).IsUnique();
                e.Property(t => t.PriceGross).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Indication>(e =>
            {
                e.HasIndex(i => i.Name).IsUnique();
            });
        }
    }
}
