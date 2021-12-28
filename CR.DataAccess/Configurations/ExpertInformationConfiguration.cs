using CR.DataAccess.Entities.IndividualInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertInformationConfiguration : IEntityTypeConfiguration<ExpertInformation>
    {
        public void Configure(EntityTypeBuilder<ExpertInformation> builder)
        {
            builder.ToTable("TBL_ExpertInformations");

            builder.HasOne(p => p.Expert)
                .WithOne(p => p.ExpertInformation)
                .HasForeignKey<ExpertInformation>(p => p.ExpertId);

            builder.HasOne(p => p.Specialty)
                .WithMany(p => p.ExpertInformations)
                .HasForeignKey(p => p.SpecialtyId);

            builder.HasMany(p => p.ExpertAppointments)
                .WithOne(p => p.ExpertInformation)
                .HasForeignKey(p => p.ExpertInformationId);

            builder.HasMany(p => p.Days)
                .WithOne(p => p.ExpertInformation)
                .HasForeignKey(p => p.ExpertInformationId);

            builder.HasMany(p => p.TimeOfDays)
                .WithOne(p => p.ExpertInformation)
                .HasForeignKey(p => p.ExpertInformationId);

            builder.HasMany(e => e.ExpertExperiences)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(e => e.ExpertImages)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(e => e.ExpertMemberships)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(e => e.ExpertPrizes)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(e => e.ExpertSubscriptions)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(e => e.ExpertStudies)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

        }
    }
}
