using CR.DataAccess.Common.Entity;

namespace CR.DataAccess.Entities.Settings
{
    public class Setting : BaseEntity
    {
        public string Title { get; set; }
        public string FavIcon { get; set; }
        public string Logo { get; set; }
        public string SiteFooterLogo { get; set; }
        public string SiteBanner { get; set; }
        public string SiteBannerColor { get; set; }
        public string TopBoxImage1 { get; set; }
        public string TopBoxText1 { get; set; }
        public string TopBoxImage2 { get; set; }
        public string TopBoxText2 { get; set; }
        public string TopBoxImage3 { get; set; }
        public string TopBoxText3 { get; set; }
    }
}