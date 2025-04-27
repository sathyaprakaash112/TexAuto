namespace TexAuto.Models.Domain.Creation
{
    public class WasteType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public Department? Department { get; set; }
        public bool IsSellable { get; set; }
    }
}
