using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models
{
    public class ProductStoreDbContext : IdentityDbContext<User, Role, string>
    {
        public ProductStoreDbContext() { }

        public ProductStoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("StoreDB"));
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.HasOne(e => e.Category)
                    .WithMany(d => d.Products)
                    .HasForeignKey(e => e.CategoryId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(e => e.UserId);
            });

            builder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.OrderId });
                entity.HasOne(e => e.Order)
                    .WithMany(d => d.OrderDetails)
                    .HasForeignKey(e => e.OrderId);
                entity.HasOne(e => e.Product)
                    .WithMany(d => d.OrderDetails)
                    .HasForeignKey(e => e.ProductId);
            });
        }
    }
}
