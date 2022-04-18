namespace CR.Core.DTOs.Days
{
    public class DayForExpertDetailsDto
    {
        public string date_String { get; set; }
        public string dayOfWeek { get; set; }
        public string StartHour { get; set; }
        public string FinishHour { get; set; }
        public bool HasTimeOfDay { get; set; }
    }
}
