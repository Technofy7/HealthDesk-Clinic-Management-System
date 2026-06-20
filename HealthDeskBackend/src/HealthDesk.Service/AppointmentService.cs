using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HealthDeskDbContext _context;
        public AppointmentService(HealthDeskDbContext context) => _context = context;

        public async Task<IEnumerable<Appointment>> GetAllAsync() =>
            await _context.Appointments
                .Include(a => a.Patient).Include(a => a.Doctor).Include(a => a.Slot)
                .AsNoTracking().ToListAsync();

        public async Task<Appointment?> GetByIdAsync(int id) =>
            await _context.Appointments
                .Include(a => a.Patient).Include(a => a.Doctor).Include(a => a.Slot)
                .FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId) =>
            await _context.Appointments.Where(a => a.PatientId == patientId).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId) =>
            await _context.Appointments.Where(a => a.DoctorId == doctorId).AsNoTracking().ToListAsync();

        public async Task<Appointment> CreateAsync(CreateAppointmentRequest request)
        {
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == request.PatientId);
            if (!patientExists)
                throw new InvalidOperationException($"Patient {request.PatientId} does not exist.");

            var slot = await _context.TimeSlots.FirstOrDefaultAsync(t => t.Id == request.SlotId);
            if (slot is null)
                throw new InvalidOperationException($"TimeSlot {request.SlotId} does not exist.");

            // If the slot exists, its DoctorId is guaranteed valid by the FK —
            // this also confirms the requested DoctorId is correct without a separate lookup.
            if (slot.DoctorId != request.DoctorId)
                throw new InvalidOperationException("The selected slot does not belong to the specified doctor.");

            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                SlotId = request.SlotId,
                Date = request.Date,
                Notes = request.Notes,
                Status = "Pending"
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<bool> UpdateAsync(int id, UpdateAppointmentRequest request)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment is null) return false;

            appointment.Status = request.Status;
            appointment.Notes = request.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment is null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
