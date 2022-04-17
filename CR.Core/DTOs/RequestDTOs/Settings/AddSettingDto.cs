using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Settings
{
    public class AddSettingDto
    {
        public string title { get; set; }
        public IFormFile favIcon { get; set; }
        public IFormFile logo { get; set; }
    }
}
