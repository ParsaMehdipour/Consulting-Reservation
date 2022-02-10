using CR.DataAccess.Entities.FinancialTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.ToTable("TBL_FinancialTransactions");

            builder.HasOne(t => t.Factor)
                .WithMany(f => f.FinancialTransactions)
                .HasForeignKey(t => t.FactorId);
        }
    }
}
