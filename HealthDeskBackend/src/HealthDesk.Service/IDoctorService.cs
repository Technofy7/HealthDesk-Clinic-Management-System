using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<Doctor> CreateAsync(CreateDoctorRequest request);
        Task<bool> UpdateAsync(int id, UpdateDoctorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
