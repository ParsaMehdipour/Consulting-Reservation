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
        }
    }
}
