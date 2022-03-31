using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class ExpertPrizeConfiguration : IEntityTypeConfiguration<ExpertPrize>
    {
        public void Configure(EntityTypeBuilder<ExpertPrize> builder)
        {
            builder.ToTable("TBL_ExpertPrizes");

            builder.HasOne(e => e.ExpertInformation)
                .WithMany(e => e.ExpertPrizes)
                .HasForeignKey(e => e.ExpertInformationId);

            builder.Property(_ => _.PrizeName)
                .HasMaxLength(100);

            builder.Property(_ => _.Year)
                .HasMaxLength(50);
        }
    }
}
