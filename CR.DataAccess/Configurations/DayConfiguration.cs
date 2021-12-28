using CR.DataAccess.Entities.ExpertAvailabilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.ToTable("TBL_Days"); 

            builder.HasMany(p => p.TimeOfDays)
                .WithOne(p => p.Day)
                .HasForeignKey(p => p.DayId);

            builder.HasOne(p => p.ExpertInformation)
                .WithMany(p => p.Days)
                .HasForeignKey(p => p.ExpertInformationId);
        }
    }
}
