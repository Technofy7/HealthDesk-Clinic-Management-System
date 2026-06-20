using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service.Dtos
{
    public record CreateMedicalRecordRequest(int AppointmentId, string? Diagnosis, string? Prescription);
    public record UpdateMedicalRecordRequest(string? Diagnosis, string? Prescription);
}
