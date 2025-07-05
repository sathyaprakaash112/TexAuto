using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Masters
{
    public class LedgerMaster
    {
        [Key]
        public int Id { get; set; }

        // SECTION 1: Basic Details
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int GroupMasterId { get; set; }

        [BindNever]
        [ForeignKey("GroupMasterId")]
        public GroupMaster? GroupMaster { get; set; } = null!;

        [Required]
        public string LedgerType { get; set; } = null!; // Supplier / Customer / Agent / Transport / General

        public bool IsActive { get; set; } = true;

        // SECTION 2: Contact & Address
        [Phone]
        public string? MobileNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        [Required]
        public string State { get; set; } = null!;

        public int? PinCode { get; set; }

        // SECTION 3: Statutory Details
        [StringLength(15)]
        public string? GstNumber { get; set; }

        [StringLength(10)]
        public string? PanNumber { get; set; }

        public bool IsTdsApplicable { get; set; }

        // SECTION 4: Opening & Financial Info
        public decimal OpeningBalance { get; set; }

        public string? BalanceType { get; set; } // Debit / Credit

        public string? AccountCode { get; set; }

        // SECTION 5: Bank Details
        public string? BankName { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? IfscCode { get; set; }

        // SECTION 7: Other Information
        public string? Remarks { get; set; }
    }
}
