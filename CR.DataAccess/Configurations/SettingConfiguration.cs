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
        }
    }
}
