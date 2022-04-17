using CR.DataAccess.Entities.ExpertAvailabilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class TimeOfDayConfiguration : IEntityTypeConfiguration<TimeOfDay>
    {
        public void Configure(EntityTypeBuilder<TimeOfDay> builder)
        {
            builder.ToTable("TBL_TimeOfDays");

            builder.HasOne(p => p.Day)
                .WithMany(p => p.TimeOfDays)
                .HasForeignKey(p => p.DayId);

            builder.HasMany(p => p.Appointments)
                .WithOne(p => p.TimeOfDay)
                .HasForeignKey(_ => _.TimeOfDayId);

            builder.HasOne(p => p.ExpertInformation)
                .WithMany(p => p.TimeOfDays)
                .HasForeignKey(p => p.ExpertInformationId);

            builder.Property(_ => _.StartHour)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.FinishHour)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(_ => _.StartTime);

            builder.Property(_ => _.FinishTime);

            builder.Property(_ => _.IsReserved);

            builder.Property(_ => _.PhoneCallPrice);

            builder.Property(_ => _.VoiceCallPrice);

            builder.Property(_ => _.TextCallPrice);

            builder.Property(_ => _.TimingType);
        }
    }
}
