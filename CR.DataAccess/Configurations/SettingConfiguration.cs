using CR.DataAccess.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("TBL_Settings");

            builder.Property(_ => _.Title);

            builder.Property(_ => _.Logo);

            builder.Property(_ => _.FavIcon);

            builder.Property(_ => _.SiteBanner);
            builder.Property(_ => _.SiteBannerColor);
            builder.Property(_ => _.SiteFooterLogo);
            builder.Property(_ => _.TopBoxImage1);
            builder.Property(_ => _.TopBoxImage2);
            builder.Property(_ => _.TopBoxImage3);
            builder.Property(_ => _.TopBoxText1);
            builder.Property(_ => _.TopBoxText2);
            builder.Property(_ => _.TopBoxText3);
            builder.Property(_ => _.UserVector);
            builder.Property(_ => _.ExpertVector);
        }
    }
}
