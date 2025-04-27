using System.Collections.Generic;
using TexAuto.Models.Domain.Creation;

namespace TexAuto.Models.Domain.Entries
{
    public class Production
    {
        public int Id { get; set; }

        // Reference to Wastes (One Production -> Many Waste records)
        public ICollection<WasteType> Wastes { get; set; }

        // Other Production-related fields
        public decimal Bale { get; set; }
        public decimal Lap { get; set; }
        public decimal DelHank { get; set; }
        public decimal ProductionEfficiency { get; set; }
        public Employee? Employee { get; set; }
        public decimal Mixing { get; set; }
        public decimal TotalProduction { get; set; }
        public decimal RunTime { get; set; }
        public decimal IdleTime { get; set; }
        public decimal NoOfDoffs { get; set; }
        public decimal ConeWeight { get; set; }
        public decimal OpeningKgs { get; set; }
        public decimal Closing { get; set; }
        public decimal SliverBreaks { get; set; }
        public decimal ProductIn { get; set; }
        public decimal ProductOut { get; set; }
        public decimal ExpectedProduction { get; set; }
        public decimal ProductionDrop { get; set; }
    }
}
