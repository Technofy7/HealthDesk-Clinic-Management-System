using HealthDesk.Data;
using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using HealthDesk.Service.Security;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HealthDesk.Service
{
    public class AuthService : IAuthService
    {
        private readonly HealthDeskDbContext _context;
        private readonly IJwtTokenService _tokenService;

        public AuthService(HealthDeskDbContext context, IJwtTokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            var emailTaken = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailTaken)
                throw new InvalidOperationException("An account with this email already exists.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = PasswordHasher.Hash(request.Password),
                Roles = "Patient"
            };

            // Two related inserts (User + Patient) — wrap in a transaction so we never
            // end up with a Patient-role User that has no matching Patients row.
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _context.Patients.Add(new Patient { UserId = user.Id });
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return user;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user is null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _tokenService.GenerateToken(user);
            return new LoginResponse(token, user.Id, user.Name, user.Roles);
        }
    }
}
