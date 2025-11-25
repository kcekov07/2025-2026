using EcoLoop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreImage> StoreImages { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Store>()
                .HasMany(s => s.Images)
                .WithOne(i => i.Store)
                .HasForeignKey(i => i.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Store>()
                .HasMany(s => s.Reviews)
                .WithOne(r => r.Store)
                .HasForeignKey(r => r.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
