using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.Images
{
    public class UploadImageDto
    {
        public string Folder { get; set; }
        public IFormFile File { get; set; }
    }
}
