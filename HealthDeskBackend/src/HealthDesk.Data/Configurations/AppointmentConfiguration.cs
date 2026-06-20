using HealthDesk.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.Property(a => a.Status).HasMaxLength(20).IsRequired().HasDefaultValue("Pending");

            builder.HasOne(a => a.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Doctor).WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Slot).WithMany(t => t.Appointments)
                .HasForeignKey(a => a.SlotId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
