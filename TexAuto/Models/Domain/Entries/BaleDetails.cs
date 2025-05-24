using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Creation
{
    public class BaleDetail
    {
        [Key]
        public int Id { get; set; }

        public int BalePurchaseId { get; set; }

        [ForeignKey("BalePurchaseId")]
        public BalePurchase? BalePurchase { get; set; }

        public int NumberOfBales { get; set; } = 0;
        public decimal NetWeightKg { get; set; } = 0;
        public decimal RatePerKg { get; set; } = 0;

        [NotMapped]
        public decimal Amount => NetWeightKg * RatePerKg;

        public decimal MoisturePercent { get; set; } = 0;
    }
}
