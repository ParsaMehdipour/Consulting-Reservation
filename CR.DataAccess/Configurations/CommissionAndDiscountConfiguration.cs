using CR.DataAccess.Entities.CommissionAndDiscounts;
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

            builder.Property(_ => _.PhoneCallCommission);

            builder.Property(_ => _.TextCallCommission);

            builder.Property(_ => _.VoiceCallCommission);

            builder.Property(_ => _.PhoneCallDiscount);

            builder.Property(_ => _.TextCallDiscount);

            builder.Property(_ => _.VoiceCallDiscount);

        }
    }
}
