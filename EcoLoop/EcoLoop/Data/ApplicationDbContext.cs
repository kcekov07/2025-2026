using EcoLoop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoLoop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Таблици за твоята домейн логика
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreImage> StoreImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<News> News { get; set; }


        // (по-късно ще добавим News, Events, Notifications и т.н.)

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

            // Примерно поле за одобрение на магазини от админ
            builder.Entity<Store>()
                .Property<bool>("IsApproved")
                .HasDefaultValue(false);
        }
    }
}
