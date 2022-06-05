using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Specialty
{
    public class RequestEditSpecialityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public IFormFile File { get; set; }
    }
}