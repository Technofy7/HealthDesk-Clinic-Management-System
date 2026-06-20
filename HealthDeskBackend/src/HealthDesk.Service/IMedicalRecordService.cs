using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecord>> GetAllAsync();
        Task<MedicalRecord?> GetByIdAsync(int id);
        Task<MedicalRecord?> GetByAppointmentIdAsync(int appointmentId);
        Task<MedicalRecord> CreateAsync(CreateMedicalRecordRequest request);
        Task<bool> UpdateAsync(int id, UpdateMedicalRecordRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
