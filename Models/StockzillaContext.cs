using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stockzilla.Models
{
    public class StockzillaContext : IdentityDbContext<StockzillaUser>
    {

        IConfigurationRoot _config;

        public StockzillaContext(IConfigurationRoot config, DbContextOptions options)
         : base(options)
        {
            _config = config;
        }

        //Loads the Class into the Entity Framework Context.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UOM> UOMs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StockReceipt> StockReceipts { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Maps Category to Product
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.CategoryId);

            //Maps UOM to Product
            modelBuilder.Entity<Product>()
            .HasOne(p => p.UOM)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.UOMId);

            //Maps Location to Product
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Location)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.LocationId);

            //Maps Location to Stock Receipt
            modelBuilder.Entity<StockReceipt>()
            .HasOne(p => p.Location);

            //Maps Product to Stock Receipt
            modelBuilder.Entity<StockReceipt>()
            .HasOne(p => p.Product);

            //Maps Location to Stock Item
            modelBuilder.Entity<StockItem>()
            .HasOne(p => p.Location)
            .WithMany(b => b.StockItems)
            .HasForeignKey(p => p.LocationId);

            //Maps Product to Stock Item
            modelBuilder.Entity<StockItem>()
            .HasOne(p => p.Product)
            .WithMany(b => b.StockItems)
            .HasForeignKey(p => p.ProductId);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionString"]);
        }
    }
}
