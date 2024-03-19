using Insert.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insert.Server.Context
{
    public class InsertContext : DbContext
    {
        public InsertContext(DbContextOptions<InsertContext> options)
            :base(options)
        {
            
        }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Currency>()
                .HasIndex(e => e.Code)
                .IsUnique();
        }
    }
}
