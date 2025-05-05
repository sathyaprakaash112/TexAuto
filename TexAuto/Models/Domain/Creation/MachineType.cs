using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TexAuto.Models.Domain.Creation
{
    public class MachineType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Foreign key
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // Navigation property
        public Department? Department { get; set; }
    }
}
