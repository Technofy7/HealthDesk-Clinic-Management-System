using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service.Dtos
{
    public record CreateAppointmentRequest(int PatientId, int DoctorId, int SlotId, DateOnly Date, string? Notes);
    public record UpdateAppointmentRequest(string Status, string? Notes);
}
