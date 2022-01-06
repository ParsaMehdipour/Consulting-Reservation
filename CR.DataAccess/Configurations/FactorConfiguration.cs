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
        }
    }
}
