using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewSpecialityDto
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
