using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly HealthDeskDbContext _context;
        public DoctorService(HealthDeskDbContext context) => _context = context;

        public async Task<IEnumerable<Doctor>> GetAllAsync() =>
            await _context.Doctors.Include(d => d.User).AsNoTracking().ToListAsync();

        public async Task<Doctor?> GetByIdAsync(int id) =>
            await _context.Doctors.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Doctor> CreateAsync(CreateDoctorRequest request)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
            if (!userExists)
                throw new InvalidOperationException($"User {request.UserId} does not exist.");

            var doctor = new Doctor
            {
                UserId = request.UserId,
                Specialization = request.Specialization,
                Bio = request.Bio
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> UpdateAsync(int id, UpdateDoctorRequest request)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor is null) return false;

            doctor.Specialization = request.Specialization;
            doctor.Bio = request.Bio;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor is null) return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
