using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId);
        Task<Appointment> CreateAsync(CreateAppointmentRequest request);
        Task<bool> UpdateAsync(int id, UpdateAppointmentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
