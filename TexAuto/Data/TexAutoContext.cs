using System;
using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.Creation;
using TexAuto.Models.Domain.Creation;
using TexAuto.Models.Domain.Entries;
using Project.Models.Domain.Masters;

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
        public DbSet<BalePurchase> BalePurchases { get; set; }
        public DbSet<BaleDetail> BaleDetails { get; set; }


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
                new Product { Id = 1, Name = "Cotton", ProductTypeId = 1, SetHank = "", NetWeight = 0.0m },
                new Product { Id = 2, Name = "Mixed Bale", ProductTypeId = 10, SetHank = "", NetWeight = 0.0m },
                new Product { Id = 3, Name = "Carding Sliver 0.092ne", ProductTypeId = 2, SetHank = "0.092ne", NetWeight = 0.0m },
                new Product { Id = 4, Name = "Breaker Sliver 0.095ne", ProductTypeId = 3, SetHank = "0.095ne", NetWeight = 0.0m },
                new Product { Id = 5, Name = "Finisher Sliver 0.097ne", ProductTypeId = 4, SetHank = "0.097ne", NetWeight = 0.0m },
                new Product { Id = 6, Name = "Roving 0.95ne", ProductTypeId = 5, SetHank = "0.95ne", NetWeight = 0.0m },
                new Product { Id = 7, Name = "Spin Yarn 60s", ProductTypeId = 6, SetHank = "60s", NetWeight = 0.0m },
                new Product { Id = 8, Name = "Autoconed Yarn 1.5Kgs 60s", ProductTypeId = 8, SetHank = "60s", NetWeight = 1.5m },
                new Product { Id = 9, Name = "Winded Yarn 1.5Kgs 60s", ProductTypeId = 7, SetHank = "60s", NetWeight = 1.5m },
                new Product { Id = 10, Name = "Autoconed Bag 60Kgs 60s", ProductTypeId = 9, SetHank = "60s", NetWeight = 60m },
                new Product { Id = 11, Name = "Winded Bag 60Kgs 60s", ProductTypeId = 9, SetHank = "60s", NetWeight = 60m }
            );

            modelBuilder.Entity<GroupMaster>().HasData(
                new GroupMaster { Id = 1, GroupName = "Capital Account", NatureOfGroup = "Capital", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 2, GroupName = "Partner’s Capital", UnderGroupId = 1, NatureOfGroup = "Capital", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 3, GroupName = "Reserves & Surplus", UnderGroupId = 1, NatureOfGroup = "Capital", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 4, GroupName = "Fixed Assets", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 5, GroupName = "Building", UnderGroupId = 4, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 6, GroupName = "Machinery", UnderGroupId = 4, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 7, GroupName = "Vehicles", UnderGroupId = 4, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 8, GroupName = "Furniture & Fixtures", UnderGroupId = 4, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 9, GroupName = "Computer Equipment", UnderGroupId = 4, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 10, GroupName = "Investments", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 11, GroupName = "Loans (Liability)", NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 12, GroupName = "Secured Loans", UnderGroupId = 11, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 13, GroupName = "Bank Loans", UnderGroupId = 12, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 14, GroupName = "Vehicle Loans", UnderGroupId = 12, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 15, GroupName = "Unsecured Loans", UnderGroupId = 11, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 16, GroupName = "Current Liabilities", NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 17, GroupName = "Sundry Creditors", UnderGroupId = 16, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 18, GroupName = "Duties & Taxes", UnderGroupId = 16, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 19, GroupName = "GST Payable", UnderGroupId = 18, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 20, GroupName = "TDS Payable", UnderGroupId = 18, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 21, GroupName = "TCS Payable", UnderGroupId = 18, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 22, GroupName = "Outstanding Expenses", UnderGroupId = 16, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 23, GroupName = "Advance from Customers", UnderGroupId = 16, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 24, GroupName = "Provisions", NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 25, GroupName = "Salary Payable", UnderGroupId = 24, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 26, GroupName = "Bonus Payable", UnderGroupId = 24, NatureOfGroup = "Liability", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 27, GroupName = "Bank Accounts", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 28, GroupName = "Cash-in-Hand", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 29, GroupName = "Cash Office", UnderGroupId = 28, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 30, GroupName = "Petty Cash", UnderGroupId = 28, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 31, GroupName = "Deposits (Assets)", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 32, GroupName = "Rent Deposit", UnderGroupId = 31, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 33, GroupName = "Security Deposit", UnderGroupId = 31, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 34, GroupName = "Current Assets", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 35, GroupName = "Sundry Debtors", UnderGroupId = 34, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 36, GroupName = "Advance to Suppliers", UnderGroupId = 34, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 37, GroupName = "Prepaid Expenses", UnderGroupId = 34, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 38, GroupName = "Input GST Credit", UnderGroupId = 34, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 39, GroupName = "TDS Receivable", UnderGroupId = 34, NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },

                new GroupMaster { Id = 40, GroupName = "Stock-in-Hand", NatureOfGroup = "Asset", AffectsInventory = true, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 41, GroupName = "Yarn", UnderGroupId = 40, NatureOfGroup = "Asset", AffectsInventory = true, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 42, GroupName = "Cotton Bale", UnderGroupId = 40, NatureOfGroup = "Asset", AffectsInventory = true, IsDefault = true, IsActive = true },
                new GroupMaster { Id = 43, GroupName = "Wastage", UnderGroupId = 40, NatureOfGroup = "Asset", AffectsInventory = true, IsDefault = true, IsActive = true },

            // (Existing entries from 1 to 43 already added)

            // 🛒 Purchase Accounts
            new GroupMaster { Id = 44, GroupName = "Purchase Accounts", NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 45, GroupName = "Yarn Purchase A/C", UnderGroupId = 44, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 46, GroupName = "Bale Purchase A/C", UnderGroupId = 44, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 47, GroupName = "Packing Material Purchase", UnderGroupId = 44, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },

            // 💸 Sales Accounts
            new GroupMaster { Id = 48, GroupName = "Sales Accounts", NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 49, GroupName = "Yarn Sales", UnderGroupId = 48, NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 50, GroupName = "Bale Sales", UnderGroupId = 48, NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 51, GroupName = "Waste Sales", UnderGroupId = 48, NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },

            // 💼 Direct Expenses
            new GroupMaster { Id = 52, GroupName = "Direct Expenses", NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 53, GroupName = "Freight Inward", UnderGroupId = 52, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 54, GroupName = "Loading Charges", UnderGroupId = 52, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 55, GroupName = "Commission Paid", UnderGroupId = 52, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 56, GroupName = "Transport Charges", UnderGroupId = 52, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },

            // 🏢 Indirect Expenses
            new GroupMaster { Id = 57, GroupName = "Indirect Expenses", NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 58, GroupName = "Rent", UnderGroupId = 57, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 59, GroupName = "Salary", UnderGroupId = 57, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 60, GroupName = "Office Expenses", UnderGroupId = 57, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 61, GroupName = "Misc. Expenses", UnderGroupId = 57, NatureOfGroup = "Expense", AffectsInventory = false, IsDefault = true, IsActive = true },

            // 📥 Indirect Incomes
            new GroupMaster { Id = 62, GroupName = "Indirect Incomes", NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 63, GroupName = "Discount Received", UnderGroupId = 62, NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 64, GroupName = "Interest Received", UnderGroupId = 62, NatureOfGroup = "Income", AffectsInventory = false, IsDefault = true, IsActive = true },

            // 🌀 Suspense
            new GroupMaster { Id = 65, GroupName = "Suspense Account", NatureOfGroup = "Asset", AffectsInventory = false, IsDefault = true, IsActive = true },

            // 🏭 Branch / Division
            new GroupMaster { Id = 66, GroupName = "Branch / Division", NatureOfGroup = "Capital", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 67, GroupName = "Unit 1", UnderGroupId = 66, NatureOfGroup = "Capital", AffectsInventory = false, IsDefault = true, IsActive = true },
            new GroupMaster { Id = 68, GroupName = "Unit 2", UnderGroupId = 66, NatureOfGroup = "Capital", AffectsInventory = false, IsDefault = true, IsActive = true }

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


        public DbSet<Project.Models.Domain.Masters.LedgerMaster> LedgerMaster { get; set; } = default!;
        public DbSet<Project.Models.Domain.Masters.GroupMaster> GroupMaster { get; set; } = default!;
    }
}
