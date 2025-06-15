using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Vouchers
{
    public class StockVoucher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StockVoucherNo { get; set; } = null!;

        [Required]
        public string StockVoucherType { get; set; } = null!; // INWARD / OUTWARD / TRANSFER

        [Required]
        [DataType(DataType.Date)]
        public DateTime StockVoucherDate { get; set; }

        public string? ReferenceNo { get; set; }

        public string? Narration { get; set; }

        public string? LinkedModule { get; set; }

        public string? LinkedRecordId { get; set; }

        public decimal TotalQuantity { get; set; }

        public decimal TotalValue { get; set; }

        public string? Location { get; set; } // Dropdown of godowns

        public string Status { get; set; } = "DRAFT"; // DRAFT / POSTED / CANCELLED

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public bool IsAuto { get; set; } = false;

        public ICollection<StockVoucherLineItem> LineItems { get; set; } = new List<StockVoucherLineItem>();
    }
}
