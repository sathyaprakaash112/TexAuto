using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TexAuto.Models.Domain.Creation;
using TexAuto.Models.Domain.Entries;

namespace TexAuto.Data
{
    public class TexAutoContext : DbContext
    {
        public TexAutoContext (DbContextOptions<TexAutoContext> options)
            : base(options)
        {
        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Waste> Wastes { get; set; }

    }
}
