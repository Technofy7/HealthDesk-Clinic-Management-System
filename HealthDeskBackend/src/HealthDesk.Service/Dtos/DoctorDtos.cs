using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service.Dtos
{
    public record CreateDoctorRequest(int UserId, string Specialization, string? Bio);
    public record UpdateDoctorRequest(string Specialization, string? Bio);
}
