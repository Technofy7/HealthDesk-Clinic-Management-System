using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string? Bio { get; set; }

        public User User { get; set; } = null!;
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
