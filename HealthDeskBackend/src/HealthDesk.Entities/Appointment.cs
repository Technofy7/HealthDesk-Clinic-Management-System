using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int SlotId { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Notes { get; set; }

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public TimeSlot Slot { get; set; } = null!;
        public MedicalRecord? MedicalRecord { get; set; }
    }
}
