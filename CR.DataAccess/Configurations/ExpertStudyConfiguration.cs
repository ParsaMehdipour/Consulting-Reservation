using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertStudyConfiguration : IEntityTypeConfiguration<ExpertStudy>
    {
        public void Configure(EntityTypeBuilder<ExpertStudy> builder)
        {
            builder.ToTable("TBL_ExpertStudies");

            builder.HasOne(e => e.ExpertInformation)
                .WithMany(e => e.ExpertStudies)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.Property(_ => _.DegreeOfEducation)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.University)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.EndDate)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
