using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Vouchers
{
    public class StockVoucherLineItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VoucherId { get; set; }

        [ForeignKey("VoucherId")]
        public StockVoucher Voucher { get; set; } = null!;

        public int? LineNo { get; set; }

        public string? LotNo { get; set; }

        public string? ItemName { get; set; } // e.g., Cotton Bale

        public decimal Quantity { get; set; }

        public int NumberOfBales { get; set; }

        public decimal RatePerKg { get; set; }

        public decimal Amount { get; set; }

        public string? Department { get; set; } // dropdown of internal locations

        public string? Remarks { get; set; }
    }
}
