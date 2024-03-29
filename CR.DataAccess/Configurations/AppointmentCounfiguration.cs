﻿using CR.DataAccess.Entities.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CR.DataAccess.Configurations
{
    public class AppointmentCounfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("TBL_Appointments");

            builder.HasOne(p => p.ConsumerInformation)
                .WithMany(p => p.ConsumerAppointments)
                .HasForeignKey(p => p.ConsumerInformationId);

            builder.HasOne(p => p.ExpertInformation)
                .WithMany(p => p.ExpertAppointments)
                .HasForeignKey(p => p.ExpertInformationId);

            builder.HasOne(p => p.TimeOfDay)
                .WithMany(p => p.Appointments)
                .HasForeignKey(p => p.TimeOfDayId);

            builder.HasOne(a => a.Factor)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.FactorId);

            builder.HasMany(a => a.ChatUsers)
                .WithOne(a => a.Appointment)
                .HasForeignKey(a => a.AppointmentId);

            builder.Property(_ => _.Reason);

            builder.Property(_ => _.AppointmentStatus);

            builder.Property(_ => _.CallingType);

            builder.Property(_ => _.Price);

            builder.Property(_ => _.RawPrice);

            builder.Property(_ => _.CommissionPrice);

            builder.Property(_ => _.DiscountPrice);
        }
    }
}
