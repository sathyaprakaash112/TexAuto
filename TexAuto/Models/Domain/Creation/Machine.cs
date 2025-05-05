using System.ComponentModel.DataAnnotations;
namespace TexAuto.Models.Domain.Creation
{
    public class Machine
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Display(Name = "Machine Type")]
        public int MachineTypeId { get; set; }

        public MachineType? MachineType { get; set; }
    }

}
