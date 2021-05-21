﻿using eCommerceStarterCode.Configuration;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eCommerceStarterCode.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<SupplierProduct> SupplierProducts { get; set; } 
        public DbSet<AppRole> AppRoles { get; set; }
        public new DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductReview> ProductReviews { get;  set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public IEnumerable<object> ShoppingCart { get; internal set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=eCommerce;Trusted_");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { CategoryId = 1, CategoryDescription = "Action Figure" });
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { ProductId = 1, ProductDescription = "Han Solo Action Figure", ProductPrice = 15, QuantityOnHand = 5, ProductAverageRating = 4, CategoryId = 1 },
                    new Product { ProductId = 2, ProductDescription = "Luke Skywalker Action Figure", ProductPrice = 15, QuantityOnHand = 5, ProductAverageRating = 4, CategoryId = 1 },
                    new Product { ProductId = 3, ProductDescription = "Darth Vader Action Figure", ProductPrice = 15, QuantityOnHand = 5, ProductAverageRating = 4, CategoryId = 1 }
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
            modelBuilder.Entity<OrderHeader>();
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });
        }

    }
}
