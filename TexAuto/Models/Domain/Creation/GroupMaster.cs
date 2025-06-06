using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Domain.Masters
{
    public class GroupMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; } = null!;

        public int? UnderGroupId { get; set; }

        [ForeignKey("UnderGroupId")]
        public GroupMaster? UnderGroup { get; set; }

        public ICollection<GroupMaster>? SubGroups { get; set; }

        public string? NatureOfGroup { get; set; }

        public bool AffectsInventory { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Remarks { get; set; }
    }
}
