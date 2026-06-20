using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }

        public Appointment Appointment { get; set; } = null!;
    }
}
