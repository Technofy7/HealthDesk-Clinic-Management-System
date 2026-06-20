using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly? DOB { get; set; }
        public string? BloodGroup { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
