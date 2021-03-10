using Microsoft.EntityFrameworkCore;

namespace CQRS.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(p => p.Iban).IsRequired().HasMaxLength(32).IsUnicode(false);
                e.Property(p => p.Type).IsRequired().HasMaxLength(10).IsUnicode(false);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
