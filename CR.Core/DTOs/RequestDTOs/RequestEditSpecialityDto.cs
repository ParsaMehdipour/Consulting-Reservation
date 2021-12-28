using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestEditSpecialityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
