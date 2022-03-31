using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertMembershipConfiguration : IEntityTypeConfiguration<ExpertMembership>
    {
        public void Configure(EntityTypeBuilder<ExpertMembership> builder)
        {
            builder.ToTable("TBL_ExpertMemberships");

            builder.HasOne(e => e.ExpertInformation)
                .WithMany(e => e.ExpertMemberships)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.Property(_ => _.Name)
                .HasMaxLength(100);
        }
    }
}
