using Microsoft.EntityFrameworkCore;
using SnapMob_Backend.Models;

namespace SnapMob_Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // User
            // -----------------------------
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // -----------------------------
            // Product
           
            // -----------------------------
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
               .HasPrecision(18, 2);

            // -----------------------------
            // Seed Brands
            // -----------------------------
            modelBuilder.Entity<ProductBrand>().HasData(
                new ProductBrand { Id = 1, Name = "Apple", CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0) },
                new ProductBrand { Id = 2, Name = "Samsung", CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0) },
                new ProductBrand { Id = 3, Name = "Xiaomi", CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0) }
            );

            // -----------------------------
            // Seed Products
            // -----------------------------
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15",
                    Description = "Latest Apple iPhone",
                    Price = 1099.99m,
                    BrandId = 1,
                    CurrentStock = 50,
                    IsActive = true,
                    CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0)
                },
                new Product
                {
                    Id = 2,
                    Name = "Galaxy S23",
                    Description = "Flagship Samsung phone",
                    Price = 999.99m,
                    BrandId = 2,
                    CurrentStock = 40,
                    IsActive = true,
                    CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0)
                }
            );

            // -----------------------------
            // Seed Product Images
            // -----------------------------
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage
                {
                    Id = 1,
                    ProductId = 1,
                    ImageUrl = "https://res.cloudinary.com/demo/image/upload/iphone15.jpg",
                    IsMain = true,
                    CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0)
                },
                new ProductImage
                {
                    Id = 2,
                    ProductId = 2,
                    ImageUrl = "https://res.cloudinary.com/demo/image/upload/galaxy_s23.jpg",
                    IsMain = true,
                    CreatedOn = new DateTime(2025, 10, 25, 0, 0, 0)
                }
            );
        }




    }
}
