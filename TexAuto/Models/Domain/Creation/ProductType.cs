using System.ComponentModel.DataAnnotations;

namespace TexAuto.Models.Domain.Creation
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public Department? Department { get; set; }
        public int  DepartmentId { get; set; }

        public bool Tradable { get; set; } = false;
    }
}