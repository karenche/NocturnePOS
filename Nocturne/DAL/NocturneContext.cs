using Microsoft.Data.Entity;

namespace Nocturne.DAL
{
    public class NocturneContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UsedProduct> UsedProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=NocturneContext.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur=> new { ur.UserId, ur.RoleId });
        }
    }
}