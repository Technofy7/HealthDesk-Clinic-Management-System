using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly HealthDeskDbContext _context;
        public TimeSlotService(HealthDeskDbContext context) => _context = context;

        public async Task<IEnumerable<TimeSlot>> GetAllAsync() =>
            await _context.TimeSlots.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TimeSlot>> GetByDoctorIdAsync(int doctorId) =>
            await _context.TimeSlots.Where(t => t.DoctorId == doctorId).AsNoTracking().ToListAsync();

        public async Task<TimeSlot?> GetByIdAsync(int id) =>
            await _context.TimeSlots.FindAsync(id);

        public async Task<TimeSlot> CreateAsync(CreateTimeSlotRequest request)
        {
            Validate(request.DayOfWeek, request.StartTime, request.EndTime);

            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == request.DoctorId);
            if (!doctorExists)
                throw new InvalidOperationException($"Doctor {request.DoctorId} does not exist.");

            var slot = new TimeSlot
            {
                DoctorId = request.DoctorId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            _context.TimeSlots.Add(slot);
            await _context.SaveChangesAsync();
            return slot;
        }

        public async Task<bool> UpdateAsync(int id, UpdateTimeSlotRequest request)
        {
            Validate(request.DayOfWeek, request.StartTime, request.EndTime);

            var slot = await _context.TimeSlots.FindAsync(id);
            if (slot is null) return false;

            slot.DayOfWeek = request.DayOfWeek;
            slot.StartTime = request.StartTime;
            slot.EndTime = request.EndTime;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var slot = await _context.TimeSlots.FindAsync(id);
            if (slot is null) return false;

            _context.TimeSlots.Remove(slot);
            await _context.SaveChangesAsync();
            return true;
        }

        private static void Validate(byte dayOfWeek, TimeOnly start, TimeOnly end)
        {
            if (dayOfWeek < 1 || dayOfWeek > 7)
                throw new InvalidOperationException("DayOfWeek must be between 1 and 7.");

            if (start >= end)
                throw new InvalidOperationException("StartTime must be before EndTime.");
        }
    }
}
