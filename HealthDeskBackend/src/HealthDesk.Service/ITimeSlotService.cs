using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface ITimeSlotService
    {
        Task<IEnumerable<TimeSlot>> GetAllAsync();
        Task<IEnumerable<TimeSlot>> GetByDoctorIdAsync(int doctorId);
        Task<TimeSlot?> GetByIdAsync(int id);
        Task<TimeSlot> CreateAsync(CreateTimeSlotRequest request);
        Task<bool> UpdateAsync(int id, UpdateTimeSlotRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
