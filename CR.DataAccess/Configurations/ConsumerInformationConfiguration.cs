using CR.DataAccess.Entities.IndividualInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ConsumerInformationConfiguration : IEntityTypeConfiguration<ConsumerInfromation>
    {
        public void Configure(EntityTypeBuilder<ConsumerInfromation> builder)
        {
            builder.ToTable("TBL_ConsumersInformations");

            builder.HasOne(p => p.Consumer)
                .WithOne(p => p.ConsumerInfromation)
                .HasForeignKey<ConsumerInfromation>(p => p.ConsumerId);

            builder.HasMany(p => p.ConsumerAppointments)
                .WithOne(p => p.ConsumerInformation)
                .HasForeignKey(p => p.ConsumerInformationId);

            builder.HasMany(c => c.Factors)
                .WithOne(c => c.ConsumerInformation)
                .HasForeignKey(c => c.ConsumerInformationId);

            builder.Property(_ => _.BirthDate_String)
                .HasMaxLength(50);

            builder.Property(_ => _.City)
                .HasMaxLength(50);

            builder.Property(_ => _.Degree)
                .HasMaxLength(50);

            builder.Property(_ => _.FirstName)
                .HasMaxLength(50);

            builder.Property(_ => _.LastName)
                .HasMaxLength(50);

            builder.Property(_ => _.Province)
                .HasMaxLength(50);

            builder.Property(_ => _.SpecificAddress)
                .HasMaxLength(150);

            builder.Property(_ => _.RowVersion)
                .IsRowVersion();
        }
    }
}
