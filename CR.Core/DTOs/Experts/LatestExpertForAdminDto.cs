namespace CR.Core.DTOs.Experts
{
    public class LatestExpertForAdminDto
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string speciality { get; set; }
        public string income { get; set; }
        public string iconSrc { get; set; }
        public long expertInformationId { get; set; }
        public int RateCount { get; set; }
        public decimal AverageRate { get; set; }

    }
}
