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
            builder.Entity<Store>().HasData(
     new Store
     {
         Id = 1,
         Name = "Bio Market",
         Category = "Еко храни",
         Latitude = 42.6977,
         Longitude = 23.3219,
         Address = "София",
         Description = "Био магазин",
         AcceptsOwnPackaging = true,
         OpeningHours = "09:00-18:00",

         ImageUrl = "/images/sample/bio.jpg"
     },
     new Store
     {
         Id = 2,
         Name = "Green Cosmetics",
         Category = "Натурална козметика",
         Latitude = 42.1479,
         Longitude = 24.7500,
         Address = "Пловдив",
         Description = "Естествена козметика",
         AcceptsOwnPackaging = false,
         OpeningHours = "09:00-19:00",

         ImageUrl = "/images/sample/cosmetics.jpg"
     }
 );


        }
    }
}
