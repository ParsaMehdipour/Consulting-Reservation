using CR.DataAccess.Entities.Appointments;
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

            builder.HasOne(p => p.Appointment)
                .WithOne(p => p.TimeOfDay)
                .HasForeignKey<TimeOfDay>(p => p.AppointmentId);

            builder.HasOne(p => p.ExpertInformation)
                .WithMany(p => p.TimeOfDays)
                .HasForeignKey(p => p.ExpertInformationId);

            builder.HasOne(p => p.Timing)
                .WithMany(p => p.TimeOfDays)
                .HasForeignKey(p => p.TimingId);
        }
    }
}
