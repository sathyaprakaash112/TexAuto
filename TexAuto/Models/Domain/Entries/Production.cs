using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TexAuto.Models.Domain.Creation;

namespace TexAuto.Models.Domain.Entries
{
    public class Production
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        public DateOnly ProductionDate { get; set; }

        [Display(Name = "Shift")]
        public int ShiftId { get; set; }

        [BindNever]
        [ForeignKey("ShiftId")]
        public Shift? Shift { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [BindNever]
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [Display(Name = "Machine")]
        public int MachineId { get; set; }

        [BindNever]
        [ForeignKey("MachineId")]
        public Machine? Machine { get; set; }

        [Display(Name = "Product In")]
        public int ProductInId { get; set; }

        [BindNever]
        [ForeignKey("ProductInId")]
        public Product? ProductIn { get; set; }

        [Display(Name = "Product Out")]
        public int ProductOutId { get; set; }

        [BindNever]
        [ForeignKey("ProductOutId")]
        public Product? ProductOut { get; set; }

        [Display(Name = "Shift Details")]
        public string? ShiftDetails { get; set; }

        [Display(Name = "Opening Hank")]
        public decimal? OpeningHank { get; set; } = 0.00m; // Nullable to support first-entry nulls

        [Display(Name = "Closing Hank")]
        public decimal? ClosingHank { get; set; } = 0.00m;  // Nullable to support first-entry nulls


        [Display(Name = "Shift Time")]
        public double ShiftTime { get; set; } = 0.0;

        [Display(Name = "Run Time")]
        public double RunTime { get; set; } = 0.0;

        [Display(Name = "Idle Time")]
        public double IdleTime { get; set; } = 0.0;

        [Display(Name = "Delivered Hank")]
        public decimal DelHank { get; set; } = 0.00m;

        [Display(Name = "Total Production")]
        public decimal TotalProduction { get; set; } = 0.00m;

        [Display(Name = "Production Efficiency")]
        public decimal ProductionEfficiency { get; set; } = 0.00m;

        [Display(Name = "Bale")]
        public decimal Bale { get; set; } = 0.00m;

        [Display(Name = "Lap")]
        public decimal Lap { get; set; } = 0.00m;

        [Display(Name = "Mixing")]
        public decimal Mixing { get; set; } = 0.00m;

        [Display(Name = "No Of Doffs")]
        public decimal NoOfDoffs { get; set; } = 0.00m;

        [Display(Name = "Cone Weight")]
        public decimal ConeWeight { get; set; } = 0.00m;

        [Display(Name = "Opening Kgs")]
        public decimal OpeningKgs { get; set; } = 0.00m;

        [Display(Name = "Closing")]
        public decimal Closing { get; set; } = 0.00m;

        [Display(Name = "Sliver Breaks")]
        public decimal SliverBreaks { get; set; } = 0.00m;

        [Display(Name = "Expected Production")]
        public decimal ExpectedProduction { get; set; } = 0.00m;

        [Display(Name = "Production Drop")]
        public decimal ProductionDrop { get; set; } = 0.00m;

        [BindNever]
        public ICollection<WasteType>? Wastes { get; set; } = new List<WasteType>();
    }
}
