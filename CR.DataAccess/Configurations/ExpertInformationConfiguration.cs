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

            builder.HasOne(p => p.CommissionAndDiscount)
                .WithOne(p => p.ExpertInformation)
                .HasForeignKey<ExpertInformation>(p => p.CommissionAndDiscountId);

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

            builder.HasMany(e => e.Factors)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(e => e.Favorites)
                .WithOne(e => e.ExpertInformation)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.HasMany(_ => _.ChatUsers)
                .WithOne(_ => _.ExpertInformation)
                .HasForeignKey(_ => _.ExpertInformationId);

            builder.Property(_ => _.Bio);

            builder.Property(_ => _.City)
                .HasMaxLength(50);

            builder.Property(_ => _.ShabaNumber)
                .HasMaxLength(50);

            builder.Property(_ => _.BirthDate_String)
                .HasMaxLength(50);

            builder.Property(_ => _.ClinicAddress)
                .HasMaxLength(200);

            builder.Property(_ => _.ClinicName)
                .HasMaxLength(50);

            builder.Property(_ => _.FirstName)
                .HasMaxLength(50);

            builder.Property(_ => _.LastName)
                .HasMaxLength(50);

            builder.Property(_ => _.Instagram)
                .HasMaxLength(50);

            builder.Property(_ => _.PostalCode)
                .HasMaxLength(50);

            builder.Property(_ => _.Gender);

            builder.Property(_ => _.Tag);

            builder.Property(_ => _.PhoneCallPrice);

            builder.Property(_ => _.VoiceCallPrice);

            builder.Property(_ => _.TextCallPrice);

            builder.Property(_ => _.SpecificAddress)
                .HasMaxLength(200);

            builder.Property(_ => _.UsePhoneCall);

            builder.Property(_ => _.UseVoiceCall);

            builder.Property(_ => _.UseTextCall);


        }
    }
}
