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
    }
}
