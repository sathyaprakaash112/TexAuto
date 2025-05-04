using TexAuto.Models.Domain.Creation;

namespace TexAuto.Models.Domain.Entries
{
    public class Waste
    {
        public int Id { get; set; }
        public WasteType WasteType { get; set; }
        public int WasteTypeId { get; set; }

        public decimal Weight { get; set; }
        public DateTime Date { get; set; }

    }
}
