using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TexAuto.Models.Domain.Creation
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public ProductType? ProductType { get; set; }

        public int ProductTypeId { get; set; }
    }
}
