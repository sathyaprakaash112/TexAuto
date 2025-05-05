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
