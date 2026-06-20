using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using HealthDesk.Service.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public class UserService : IUserService
    {
        private readonly HealthDeskDbContext _context;
        public UserService(HealthDeskDbContext context) => _context = context;

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<User> CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Roles = request.Roles,
                PasswordHash = PasswordHasher.Hash(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return false;

            user.Name = request.Name;
            user.Email = request.Email;
            user.Roles = request.Roles;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
