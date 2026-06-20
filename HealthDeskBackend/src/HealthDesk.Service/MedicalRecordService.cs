using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly HealthDeskDbContext _context;
        public MedicalRecordService(HealthDeskDbContext context) => _context = context;

        public async Task<IEnumerable<MedicalRecord>> GetAllAsync() =>
            await _context.MedicalRecords.AsNoTracking().ToListAsync();

        public async Task<MedicalRecord?> GetByIdAsync(int id) =>
            await _context.MedicalRecords.FindAsync(id);

        public async Task<MedicalRecord?> GetByAppointmentIdAsync(int appointmentId) =>
            await _context.MedicalRecords.FirstOrDefaultAsync(m => m.AppointmentId == appointmentId);

        public async Task<MedicalRecord> CreateAsync(CreateMedicalRecordRequest request)
        {
            var appointmentExists = await _context.Appointments.AnyAsync(a => a.Id == request.AppointmentId);
            if (!appointmentExists)
                throw new InvalidOperationException($"Appointment {request.AppointmentId} does not exist.");

            var record = new MedicalRecord
            {
                AppointmentId = request.AppointmentId,
                Diagnosis = request.Diagnosis,
                Prescription = request.Prescription
            };

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<bool> UpdateAsync(int id, UpdateMedicalRecordRequest request)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record is null) return false;

            record.Diagnosis = request.Diagnosis;
            record.Prescription = request.Prescription;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record is null) return false;

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
