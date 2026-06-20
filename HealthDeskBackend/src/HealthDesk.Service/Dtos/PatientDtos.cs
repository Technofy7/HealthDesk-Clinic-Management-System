using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service.Dtos
{
    public record CreatePatientRequest(int UserId, DateOnly? DOB, string? BloodGroup);
    public record UpdatePatientRequest(DateOnly? DOB, string? BloodGroup);
}
