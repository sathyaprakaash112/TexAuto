using System.ComponentModel.DataAnnotations;

namespace TexAuto.Models.Domain.Creation
{
    public class Machine
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        public MachineType? Type { get; set; }
    }
}
