using HealthDesk.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Data
{
    public class HealthDeskDbContext : DbContext
    {
        public HealthDeskDbContext(DbContextOptions<HealthDeskDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthDeskDbContext).Assembly);
        }
    }
}
