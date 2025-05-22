using System;
using Microsoft.EntityFrameworkCore;
using TexAuto.Models.Domain.Creation;
using TexAuto.Models.Domain.Entries;

namespace TexAuto.Data
{
    public class TexAutoContext : DbContext
    {
        public TexAutoContext(DbContextOptions<TexAutoContext> options)
            : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Waste> Wastes { get; set; }
        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Mixing", Description = "Mixing" },
                new Department { Id = 2, Name = "Blowroom", Description = "Blowroom" },
                new Department { Id = 3, Name = "Carding", Description = "Carding" },
                new Department { Id = 4, Name = "Drawing Breaker", Description = "Drawing Breaker" },
                new Department { Id = 5, Name = "Drawing Finisher", Description = "Drawing Finisher" },
                new Department { Id = 6, Name = "Simplex", Description = "Simplex" },
                new Department { Id = 7, Name = "Spinning", Description = "Spinning" },
                new Department { Id = 8, Name = "Cone Winding", Description = "Cone Winding" },
                new Department { Id = 9, Name = "Autoconer", Description = "Autoconer" },
                new Department { Id = 10, Name = "Packing", Description = "Packing" },
                new Department { Id = 11, Name = "Sweeping", Description = "Sweeping" },
                new Department { Id = 12, Name = "Extra Work", Description = "Extra Work" },
                new Department { Id = 13, Name = "Others", Description = "Others" }
            );

            // MachineTypes
            modelBuilder.Entity<MachineType>().HasData(
                new MachineType { Id = 1, Name = "Bale Plucker", DepartmentId = 1 },
                new MachineType { Id = 2, Name = "Blowroom", DepartmentId = 2 },
                new MachineType { Id = 3, Name = "Carding", DepartmentId = 3 },
                new MachineType { Id = 4, Name = "Drawing Breaker", DepartmentId = 4 },
                new MachineType { Id = 5, Name = "Drawing Finisher", DepartmentId = 5 },
                new MachineType { Id = 6, Name = "Simplex", DepartmentId = 6 },
                new MachineType { Id = 7, Name = "Spinning", DepartmentId = 7 },
                new MachineType { Id = 8, Name = "Cone Winding", DepartmentId = 8 },
                new MachineType { Id = 9, Name = "Autoconer", DepartmentId = 9 }
            );

            // ProductTypes
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Cotton", DepartmentId = 13 },
                new ProductType { Id = 2, Name = "Carding Sliver", DepartmentId = 3 },
                new ProductType { Id = 3, Name = "Breaker Sliver", DepartmentId = 4 },
                new ProductType { Id = 4, Name = "Finisher Sliver", DepartmentId = 5 },
                new ProductType { Id = 5, Name = "Roving", DepartmentId = 6 },
                new ProductType { Id = 6, Name = "Spin Yarn", DepartmentId = 7 },
                new ProductType { Id = 7, Name = "Winded Yarn", DepartmentId = 8 },
                new ProductType { Id = 8, Name = "Autoconed Yarn", DepartmentId = 9 },
                new ProductType { Id = 9, Name = "Bag", DepartmentId = 10 },
                new ProductType { Id = 10, Name = "Mixed Bale", DepartmentId = 1 }

            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Cotton", ProductTypeId = 1, SetHank = "", NetWeight = null },
                new Product { Id = 2, Name = "Mixed Bale", ProductTypeId = 10, SetHank = "", NetWeight = null },
                new Product { Id = 3, Name = "Carding Sliver", ProductTypeId = 2, SetHank = "0.092ne", NetWeight = null },
                new Product { Id = 4, Name = "Breaker Sliver", ProductTypeId = 3, SetHank = "0.095ne", NetWeight = null },
                new Product { Id = 5, Name = "Finisher Sliver", ProductTypeId = 4, SetHank = "0.097ne", NetWeight = null },
                new Product { Id = 6, Name = "Roving", ProductTypeId = 5, SetHank = "0.95ne", NetWeight = null },
                new Product { Id = 7, Name = "Spin Yarn", ProductTypeId = 6, SetHank = "60s", NetWeight = null },
                new Product { Id = 8, Name = "Autoconed Yarn", ProductTypeId = 8, SetHank = "60s", NetWeight = 1.5m },
                new Product { Id = 9, Name = "Winded Yarn", ProductTypeId = 7, SetHank = "60s", NetWeight = 1.5m },
                new Product { Id = 10, Name = "Autoconed Bag", ProductTypeId = 9, SetHank = "60s", NetWeight = 60m },
                new Product { Id = 11, Name = "Winded Bag", ProductTypeId = 9, SetHank = "60s", NetWeight = 60m }
            );


            modelBuilder.Entity<Shift>().HasData(
                new Shift
                {
                    Id = 1,
                    EffectiveDate = new DateOnly(2024, 1, 1), // You can adjust this
                    TotalShifts = 2,
                    StartTime1 = new TimeOnly(8, 0),
                    EndTime1 = new TimeOnly(20, 0),
                    StartTime2 = new TimeOnly(20, 0),
                    EndTime2 = new TimeOnly(8, 0)
                }
);


            ConfigureProduction(modelBuilder);
        }

        private void ConfigureProduction(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Production>();

            // Relationships with restricted deletes
            entity.HasOne(p => p.ProductIn)
                  .WithMany()
                  .HasForeignKey(p => p.ProductInId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.ProductOut)
                  .WithMany()
                  .HasForeignKey(p => p.ProductOutId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Machine)
                  .WithMany()
                  .HasForeignKey(p => p.MachineId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Department)
                  .WithMany()
                  .HasForeignKey(p => p.DepartmentId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Shift)
                  .WithMany()
                  .HasForeignKey(p => p.ShiftId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Optional: Indexes for query performance
            entity.HasIndex(p => p.ProductionDate);
            entity.HasIndex(p => p.DepartmentId);
        }
    }
}
