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

            builder.Property(_ => _.CardHolderPAN);

            builder.Property(_ => _.CreateDate_String);

            builder.Property(_ => _.PayerId);

            builder.Property(_ => _.Price_Digit);

            builder.Property(_ => _.Price_String);

            builder.Property(_ => _.SaleReferenceId);

            builder.Property(_ => _.ReceiverId);

            builder.Property(_ => _.RefId);

            builder.Property(_ => _.TransactionNumber);

            builder.Property(_ => _.TransactionType);

            builder.Property(_ => _.Status);
        }
    }
}
