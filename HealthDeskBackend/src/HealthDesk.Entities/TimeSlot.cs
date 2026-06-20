using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Entities
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public byte DayOfWeek { get; set; } // 1–7
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public Doctor Doctor { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
