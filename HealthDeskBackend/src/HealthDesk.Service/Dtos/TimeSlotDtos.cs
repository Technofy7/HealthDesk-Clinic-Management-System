using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service.Dtos
{
    public record CreateTimeSlotRequest(int DoctorId, byte DayOfWeek, TimeOnly StartTime, TimeOnly EndTime);
    public record UpdateTimeSlotRequest(byte DayOfWeek, TimeOnly StartTime, TimeOnly EndTime);
}
