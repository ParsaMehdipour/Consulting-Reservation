using System.ComponentModel.DataAnnotations;

namespace CR.Core.DTOs.Settings
{
    public class SettingDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "لطفا عنوان سایت را وارد کنید")]
        public string Title { get; set; }
        public string FavIcon { get; set; }
        public string Logo { get; set; }
        public string FooterLogo { get; set; }
        public string Banner { get; set; }
        public string BannerColor { get; set; }
        public string TopBoxImage1 { get; set; }
        public string TopBoxText1 { get; set; }
        public string TopBoxImage2 { get; set; }
        public string TopBoxText2 { get; set; }
        public string TopBoxImage3 { get; set; }
        public string TopBoxText3 { get; set; }
    }
}