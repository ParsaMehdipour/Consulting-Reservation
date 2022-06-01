namespace CR.Core.DTOs.Specialities
{
    public class SpecialityDto
    {
        public long Id { get; set; }
        public SpecialityDto Parent { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public bool HasChildren { get; set; }
    }
}
