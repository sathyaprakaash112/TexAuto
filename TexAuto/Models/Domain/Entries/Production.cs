using System;
using System.Collections.Generic;
using TexAuto.Models.Domain.Creation;

namespace TexAuto.Models.Domain.Entries
{
    public class Production
    {
        public int Id { get; set; }

        // Dates
        public DateOnly ProductionDate { get; set; } // Main production date

        // Relationships
        public int ShiftId { get; set; } // FK for Shift (Radio button)
        public Shift Shift { get; set; }

        public int DepartmentId { get; set; } // Dropdown
        public Department Department { get; set; }

        public int MachineId { get; set; } // Dropdown
        public Machine Machine { get; set; }

        // Shift info
        public string? ShiftDetails { get; set; } // e.g., Shift 1 (1:00 - 2:00)

        // Time tracking
        public double ShiftTime { get; set; } // Hours
        public double RunTime { get; set; }   // Hours
        public double IdleTime { get; set; }  // Hours

        // Production metrics
        public decimal DelHank { get; set; } // Decimal
        public decimal TotalProduction { get; set; } // Decimal
        public decimal ProductionEfficiency { get; set; } // %

        // Waste entries (Subform)
        public ICollection<WasteType> Wastes { get; set; } = new List<WasteType>();

        // Products
        public int ProductInId { get; set; } // Dropdown
        public Product ProductIn { get; set; }

        public int ProductOutId { get; set; } // Dropdown
        public Product ProductOut { get; set; }

        // Extra fields (from original, retained if needed)
        public decimal Bale { get; set; }
        public decimal Lap { get; set; }
        public decimal Mixing { get; set; }
        public decimal NoOfDoffs { get; set; }
        public decimal ConeWeight { get; set; }
        public decimal OpeningKgs { get; set; }
        public decimal Closing { get; set; }
        public decimal SliverBreaks { get; set; }
        public decimal ExpectedProduction { get; set; }
        public decimal ProductionDrop { get; set; }
    }
}
