namespace CR.Core.DTOs.Experts
{
    public class ExpertForPresentationDto
    {
        public long Id { get; set; }
        public long ExpertInformationId { get; set; }
        public string IconSrc { get; set; }
        public string FullName { get; set; }
        public string Tags { get; set; }
        public string SpecialitySrc { get; set; }
        public string Speciality { get; set; }
        public string FirstAvailability { get; set; }
        public string Price { get; set; }
        public bool HasStar { get; set; }
    }
}
