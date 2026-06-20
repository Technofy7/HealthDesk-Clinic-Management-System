using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> CreateAsync(CreatePatientRequest request);
        Task<bool> UpdateAsync(int id, UpdatePatientRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
