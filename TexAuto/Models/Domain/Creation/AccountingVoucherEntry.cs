using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Accounting
{
    public class AccountingVoucherEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VoucherId { get; set; }

        [ForeignKey("VoucherId")]
        public AccountingVoucherHeader VoucherHeader { get; set; } = null!;

        public int LineNo { get; set; }

        [Required]
        public string Ledger { get; set; } = null!; // Lookup from LedgerMaster

        [Required]
        public string DrCr { get; set; } = null!; // "Dr" or "Cr"

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string? Remarks { get; set; }

        public bool IsTaxLine { get; set; }
    }
}
