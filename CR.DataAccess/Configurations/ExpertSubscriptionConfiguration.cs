using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertSubscriptionConfiguration : IEntityTypeConfiguration<ExpertSubscription>
    {
        public void Configure(EntityTypeBuilder<ExpertSubscription> builder)
        {
            builder.ToTable("TBL_ExpertSubscriptions");

            builder.HasOne(e => e.ExpertInformation)
                .WithMany(e => e.ExpertSubscriptions)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.Property(_ => _.SubscriptionName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(_ => _.Year)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
