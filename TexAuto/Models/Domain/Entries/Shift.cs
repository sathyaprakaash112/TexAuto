namespace TexAuto.Models.Domain.Entries
{
    public class Shift
    {
        public int Id { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int TotalShifts { get; set; }

        public DateTime StartTime1 { get; set; }
        public DateTime EndTime1 { get; set; }
        public DateTime StartTime2 { get; set; }
        public DateTime EndTime2 { get; set; }
        public DateTime StartTime3 { get; set; }
        public DateTime EndTime3 { get; set; }
        public DateTime StartTime4 { get; set; }
        public DateTime EndTime4 { get; set; }

    }
}
