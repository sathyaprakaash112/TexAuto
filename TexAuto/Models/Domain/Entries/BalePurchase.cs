using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Creation
{
    public class BalePurchase
    {
        [Key]
        public int Id { get; set; }

        public int InwardNo { get; set; } // Increment manually
        public DateTime InwardDate { get; set; } = DateTime.Now;
        public string? PurchaseType { get; set; }
        public string? Supplier { get; set; }
        public string? BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string? LotNo { get; set; }
        public string? BaleType { get; set; }

        // Charges & Logistics
        public string? PurchaseAccount { get; set; }
        public string? Agent { get; set; }
        public decimal CommissionPerBale { get; set; } = 0;

        [NotMapped]
        public decimal CommissionAmount => CommissionPerBale * (BaleDetails?.Sum(b => b.NumberOfBales) ?? 0);

        public string? TransportVendor { get; set; }
        public decimal FreightCharges { get; set; } = 0;
        public decimal UnloadingCharges { get; set; } = 0;
        public string? DeliveryAt { get; set; }
        public int DueDays { get; set; } = 0;
        public string? Remarks { get; set; }

        // Tax & Total
        [NotMapped]
        public decimal TaxableAmount => BaleDetails?.Sum(b => b.NetWeightKg * b.RatePerKg) ?? 0;

        public decimal CgstPercent { get; set; } = 2.5M;
        [NotMapped]
        public decimal CgstAmount => TaxableAmount * CgstPercent / 100;

        public decimal SgstPercent { get; set; } = 2.5M;
        [NotMapped]
        public decimal SgstAmount => TaxableAmount * SgstPercent / 100;

        public decimal IgstPercent { get; set; } = 0;
        [NotMapped]
        public decimal IgstAmount => TaxableAmount * IgstPercent / 100;

        public decimal TcsAmount { get; set; } = 0;
        public decimal RoundOff { get; set; } = 0;

        [NotMapped]
        public decimal NetAmount => TaxableAmount + CgstAmount + SgstAmount + IgstAmount + TcsAmount - RoundOff;

        public decimal TdsPercent { get; set; } = 0;
        [NotMapped]
        public decimal TdsAmount => NetAmount * TdsPercent / 100;

        [NotMapped]
        public decimal PayableAmount => NetAmount - TdsAmount;

        // Stock Tracking
        public int BaleOut { get; set; } = 0;
        public int BaleUsed { get; set; } = 0;

        [NotMapped]
        public int TotalBales => BaleDetails?.Sum(b => b.NumberOfBales) ?? 0;
        [NotMapped]
        public int ClosingBale => TotalBales - BaleOut - BaleUsed;

        [NotMapped]
        public decimal AvgBaleWeight
            => TotalBales > 0 ? (BaleDetails?.Sum(b => b.NetWeightKg) ?? 0) / TotalBales : 0;

        [NotMapped]
        public decimal ClosingBaleKg => AvgBaleWeight * ClosingBale;

        // Weighbridge
        public string? WeighbridgeSlipNo { get; set; }
        public decimal GrossWeightKg { get; set; } = 0;
        public decimal TareWeightKg { get; set; } = 0;

        [NotMapped]
        public decimal WeighbridgeNetWeightKg => GrossWeightKg - TareWeightKg;

        // Navigation
        public ICollection<BaleDetail> BaleDetails { get; set; } = new List<BaleDetail>();
    }
}
