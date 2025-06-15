using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Domain.Accounting
{
    public class AccountingVoucherHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VoucherNo { get; set; } = null!;

        [Required]
        public string VoucherType { get; set; } = null!; // PURCHASE, JOURNAL, etc.

        [Required]
        [DataType(DataType.Date)]
        public DateTime VoucherDate { get; set; }

        public string? ReferenceNo { get; set; }

        public string? Narration { get; set; }

        public string? PartyLedger { get; set; } // Optional, may also link to LedgerMaster if needed

        public string? LinkedModule { get; set; } // e.g., BALE_PURCHASE

        public string? LinkedRecordId { get; set; } // Foreign ref

        [DataType(DataType.Currency)]
        public decimal TotalDebit { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalCredit { get; set; }

        [Required]
        public string Status { get; set; } = "DRAFT"; // DRAFT / POSTED / CANCELLED

        public string? CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public bool IsAuto { get; set; }

        public ICollection<AccountingVoucherEntry> Entries { get; set; } = new List<AccountingVoucherEntry>();
    }
}
