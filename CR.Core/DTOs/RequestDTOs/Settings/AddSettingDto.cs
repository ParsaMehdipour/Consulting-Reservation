using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Settings
{
    public class AddSettingDto
    {
        public string title { get; set; }
        public IFormFile favIcon { get; set; }
        public IFormFile logo { get; set; }

        public IFormFile banner { get; set; }
        public string bannercolor { get; set; }
        public IFormFile footer { get; set; }
        public IFormFile imagebox1 { get; set; }
        public string textbox1 { get; set; }
        public IFormFile imagebox2 { get; set; }
        public string textbox2 { get; set; }
        public IFormFile imagebox3 { get; set; }
        public string textbox3 { get; set; }
        public IFormFile UserVector { get; set; }
        public IFormFile ExpertVector { get; set; }
    }
}