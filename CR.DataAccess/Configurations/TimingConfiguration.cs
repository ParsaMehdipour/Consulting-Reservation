using CR.DataAccess.Entities.Timings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class TimingConfiguration : IEntityTypeConfiguration<Timing>
    {
        public void Configure(EntityTypeBuilder<Timing> builder)
        {
            builder.ToTable("TBL_Timings");

            builder.Property(_ => _.StartTime_String)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.EndTime_String)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.StartTime);

            builder.Property(_ => _.EndTime);

            builder.Property(_ => _.TimingType);
        }
    }
}
