using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertExperienceConfiguration : IEntityTypeConfiguration<ExpertExperience>
    {
        public void Configure(EntityTypeBuilder<ExpertExperience> builder)
        {
            builder.ToTable("TBL_ExpertExperiences");

            builder.HasOne(e => e.ExpertInformation)
                .WithMany(e => e.ExpertExperiences)
                .HasForeignKey(e => e.ExpertInformationId);
        }
    }
}
