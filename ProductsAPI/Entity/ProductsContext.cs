using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProductsAPI.Models;

namespace ProductsAPI.Entity
{
    public class ProductsContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ürün verilerini ekleyin
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "iPhone 14", Price = 1000, IsActive = true },
                new Product { ProductId = 2, ProductName = "iPhone 15", Price = 2000, IsActive = true },
                new Product { ProductId = 3, ProductName = "iPhone 16", Price = 3000, IsActive = false },
                new Product { ProductId = 4, ProductName = "iPhone 17", Price = 4000, IsActive = true },
                new Product { ProductId = 5, ProductName = "iPhone 18", Price = 5000, IsActive = false },
                new Product { ProductId = 6, ProductName = "iPhone 19", Price = 6000, IsActive = true }
            );

            // Roller için verileri ekleyin
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            // Kullanıcılar için verileri ekleyin
            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    UserName = "admin@products.com",
                    NormalizedUserName = "ADMIN@PRODUCTS.COM",
                    Email = "admin@products.com",
                    NormalizedEmail = "ADMIN@PRODUCTS.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"),
                    SecurityStamp = string.Empty,
                    FullName = "Admin User" // FullName sağlandı
                },
                new AppUser
                {
                    Id = "2",
                    UserName = "user@products.com",
                    NormalizedUserName = "USER@PRODUCTS.COM",
                    Email = "user@products.com",
                    NormalizedEmail = "USER@PRODUCTS.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User123!"),
                    SecurityStamp = string.Empty,
                    FullName = "Regular User" // FullName sağlandı
                }
            );

            // Kullanıcı-Rol ilişkisini tanımlayın
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Admin rolü
                new IdentityUserRole<string> { UserId = "2", RoleId = "2" }  // User rolü
            );
        }

        public DbSet<Product> Products { get; set; }
    }
}
