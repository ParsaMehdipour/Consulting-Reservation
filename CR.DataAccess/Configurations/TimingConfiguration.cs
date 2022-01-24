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


        }
    }
}
