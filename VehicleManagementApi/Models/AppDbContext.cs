using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleManagementModels;

namespace VehicleManagementApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>(eb =>
            {
                eb.Property(v => v.Weight).HasColumnType("decimal(8, 2)");
                eb.Property(v => v.OwnerName).IsRequired();
            });
            modelBuilder.Entity<Category>(eb =>
            {
                eb.Property(c => c.WeightFrom).HasColumnType("decimal(8, 2)");
                eb.Property(c => c.WeightUpTo).HasColumnType("decimal(8, 2)");
                eb.Property(c => c.CategoryName).IsRequired();
                eb.Property(c => c.Icon).IsRequired();
            });
            //Seed Category
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Light", WeightFrom = 0.00m, WeightUpTo = 500.00m, Icon = "fas fa-motorcycle" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Medium", WeightFrom = 500.00m, WeightUpTo = 2500.00m, Icon = "fas fa-car" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Heavy", WeightFrom = 2500.00m, WeightUpTo = 100000.00m, Icon = "fas fa-truck" });

            //Seed Vehicle
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { VehicleId = 1, OwnerName = "Owner1", Manufacturer = Manufacturer.Ferrari, ManufactureYear = 2020, Weight = 100.00m });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { VehicleId = 2, OwnerName = "Owner2", Manufacturer = Manufacturer.Honda, ManufactureYear = 1998, Weight = 500.00m });
            modelBuilder.Entity<Vehicle>().HasData(new Vehicle { VehicleId = 3, OwnerName = "Owner3", Manufacturer = Manufacturer.Toyota, ManufactureYear = 1899, Weight = 5000.00m });

        }
    }
}
