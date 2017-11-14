using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConectionsString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(a =>
            {
                a.Property(b => b.Name).HasColumnType("nvarchar(50)");
                a.Property(b => b.Description).HasColumnType("nvarchar(250)").HasDefaultValue("No description");
                a.HasKey(x => x.ProductId);
                a.HasMany(x => x.Sales)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            });

            builder.Entity<Customer>(a =>
            {
                a.Property(b => b.Name).HasColumnType("nvarchar(100)");
                a.Property(b => b.Email).HasColumnType("varchar(80)");
                a.HasKey(x => x.CustomerId);
                a.HasMany(x => x.Sales)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
            });

            builder.Entity<Store>(a =>
            {
                a.Property(b => b.Name).HasColumnType("nvarchar(80)");
                a.HasKey(x => x.StoreId);
                a.HasMany(x => x.Sales)
                .WithOne(x => x.Store)
                .HasForeignKey(x => x.StoreId);
            });

            builder.Entity<Sale>(a =>
            {
                a.HasKey(x => x.SaleId);
                a.Property(x => x.Date).HasDefaultValueSql("GETDATE()");
            });


        }
    }
}
