using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertImageConfiguration : IEntityTypeConfiguration<ExpertImage>
    {
        public void Configure(EntityTypeBuilder<ExpertImage> builder)
        {
            builder.ToTable("TBL_ExpertImages");

            builder.HasOne(e => e.ExpertInformation)
                .WithMany(e => e.ExpertImages)
                .HasForeignKey(e => e.ExpertInformationId);
        }
    }
}
