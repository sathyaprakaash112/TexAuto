using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TexAuto.Models.Domain.Creation;

namespace TexAuto.Models.Domain.Entries
{
    public class Production
    {
        public int Id { get; set; }

        // Dates
        public DateOnly ProductionDate { get; set; }

        // Relationships
        public int ShiftId { get; set; }

        [ForeignKey("ShiftId")]
        public Shift Shift { get; set; } = null!;

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; } = null!;

        public int MachineId { get; set; }

        [ForeignKey("MachineId")]
        public Machine Machine { get; set; } = null!;

        public int ProductInId { get; set; }

        [ForeignKey("ProductInId")]
        public Product ProductIn { get; set; } = null!;

        public int ProductOutId { get; set; }

        [ForeignKey("ProductOutId")]
        public Product ProductOut { get; set; } = null!;

        // Shift info
        public string? ShiftDetails { get; set; }

        // Time tracking
        public double ShiftTime { get; set; } = 0.0;
        public double RunTime { get; set; } = 0.0;
        public double IdleTime { get; set; } = 0.0;

        // Production metrics
        public decimal DelHank { get; set; } = 0.00m;
        public decimal TotalProduction { get; set; } = 0.00m;
        public decimal ProductionEfficiency { get; set; } = 0.00m;

        // Extra fields
        public decimal Bale { get; set; } = 0.00m;
        public decimal Lap { get; set; } = 0.00m;
        public decimal Mixing { get; set; } = 0.00m;
        public decimal NoOfDoffs { get; set; } = 0.00m;
        public decimal ConeWeight { get; set; } = 0.00m;
        public decimal OpeningKgs { get; set; } = 0.00m;
        public decimal Closing { get; set; } = 0.00m;
        public decimal SliverBreaks { get; set; } = 0.00m;
        public decimal ExpectedProduction { get; set; } = 0.00m;
        public decimal ProductionDrop { get; set; } = 0.00m;

        // Waste entries (optional subform logic)
        public ICollection<WasteType>? Wastes { get; set; } = new List<WasteType>();
    }
}
