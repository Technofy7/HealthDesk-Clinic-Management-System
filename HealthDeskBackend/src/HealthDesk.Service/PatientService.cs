using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public class PatientService : IPatientService
    {
        private readonly HealthDeskDbContext _context;
        public PatientService(HealthDeskDbContext context) => _context = context;

        public async Task<IEnumerable<Patient>> GetAllAsync() =>
            await _context.Patients.Include(p => p.User).AsNoTracking().ToListAsync();

        public async Task<Patient?> GetByIdAsync(int id) =>
            await _context.Patients.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Patient> CreateAsync(CreatePatientRequest request)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
            if (!userExists)
                throw new InvalidOperationException($"User {request.UserId} does not exist.");

            var patient = new Patient
            {
                UserId = request.UserId,
                DOB = request.DOB,
                BloodGroup = request.BloodGroup
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> UpdateAsync(int id, UpdatePatientRequest request)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient is null) return false;

            patient.DOB = request.DOB;
            patient.BloodGroup = request.BloodGroup;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient is null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
