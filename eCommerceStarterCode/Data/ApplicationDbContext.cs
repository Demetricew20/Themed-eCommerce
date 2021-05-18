using eCommerceStarterCode.Configuration;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eCommerceStarterCode.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<SupplierProduct> SupplierProducts { get; set; } 
        public  DbSet<AppRole> AppRoles { get; set; }
        public  DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductReview> ProductReviews { get;  set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public IEnumerable<object> ShoppingCart { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=eCommerce;Trusted_");
            }
        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//              //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ECommerce;Trusted_Connection=True;");
//            }
//        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
            .HasData(
            new Product { ProductId = 1, ProductDescription = "Han Solo Action Figure", ProductPrice = 15, QuantityOnHand = 5, ProductAverageRating = 4 }  
            );

            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.Entity<SupplierProduct>().HasKey(sp => new { sp.UserId, sp.ProductId });
            modelBuilder.Entity<User>();
            modelBuilder.Entity<ShoppingCart>().HasKey(u => new { u.UserId, u.ProductId });
            modelBuilder.Entity<UserRole>().HasKey(u => new { u.UserId, u.RoleId });
            modelBuilder.Entity<AppRole>()
                .HasData(
                    new AppRole { RoleId = 1, RoleName= "Customer" },
                    new AppRole { RoleId = 2, RoleName= "Supplier" },
                    new AppRole { RoleId = 3, RoleName= "Admin" }

                ); 
        }

    }
}
