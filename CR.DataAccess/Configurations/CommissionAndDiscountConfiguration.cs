using CR.DataAccess.Entities.CommissionAndDiscounts;
using CR.DataAccess.Entities.IndividualInformations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class CommissionAndDiscountConfiguration : IEntityTypeConfiguration<CommissionAndDiscount>
    {
        public void Configure(EntityTypeBuilder<CommissionAndDiscount> builder)
        {
            builder.ToTable("TBL_CommissionAndDiscounts");

            builder.HasOne(p => p.ExpertInformation)
                .WithOne(p => p.CommissionAndDiscount)
                .HasForeignKey<CommissionAndDiscount>(p => p.ExpertInformationId);
        }
    }
}
