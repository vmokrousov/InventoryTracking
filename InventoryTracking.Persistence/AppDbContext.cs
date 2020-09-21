using InventoryTracking.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace InventoryTracking.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventory>().HasData(
               new Inventory { Id = 1, Name = "Apples", Quantity = 3,  CreatedOn = DateTime.Now },
               new Inventory { Id = 2, Name = "Oranges", Quantity = 7, CreatedOn = DateTime.Now },
               new Inventory { Id = 3, Name = "Pomegranates", Quantity = 55, CreatedOn = DateTime.Now }
           );

        }

        /// <summary>
        /// Inventory
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }
    }
}
