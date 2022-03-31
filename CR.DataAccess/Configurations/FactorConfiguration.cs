using CR.DataAccess.Entities.Factors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class FactorConfiguration : IEntityTypeConfiguration<Factor>
    {
        public void Configure(EntityTypeBuilder<Factor> builder)
        {
            builder.ToTable("TBL_Factors");

            builder.HasMany(f => f.Appointments)
                .WithOne(f => f.Factor)
                .HasForeignKey(f => f.FactorId);

            builder.HasOne(f => f.ExpertInformation)
                .WithMany(f => f.Factors)
                .HasForeignKey(f => f.ExpertInformationId);

            builder.HasOne(f => f.ConsumerInformation)
                .WithMany(f => f.Factors)
                .HasForeignKey(f => f.ConsumerInformationId);

            builder.HasMany(f => f.FinancialTransactions)
                .WithOne(f => f.Factor)
                .HasForeignKey(f => f.FactorId);

            builder.Property(_ => _.FactorNumber);

            builder.Property(_ => _.CardHolderPAN);

            builder.Property(_ => _.FactorStatus);

            builder.Property(_ => _.RefId);

            builder.Property(_ => _.SaleReferenceId);

            builder.Property(_ => _.TotalPrice);
        }
    }
}
