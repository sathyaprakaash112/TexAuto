namespace TexAuto.Models.Domain.Entries
{
    public class Shift
    {
        public int Id { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public int TotalShifts { get; set; }

        public TimeOnly? StartTime1 { get; set; } // Default to 08:00 AM
        public TimeOnly? EndTime1 { get; set; }     // Default to 04:00 PM

        public TimeOnly? StartTime2 { get; set; }     // Default to 09:00 AM
        public TimeOnly? EndTime2 { get; set; }       // Default to 05:00 PM

        public TimeOnly? StartTime3 { get; set; }   // Default to 10:00 AM
        public TimeOnly? EndTime3 { get; set; }     // Default to 06:00 PM

        public TimeOnly? StartTime4 { get; set; }  // Default to 11:00 AM
        public TimeOnly? EndTime4 { get; set; }     // Default to 07:00 PM


    }
}
