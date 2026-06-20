using HealthDesk.Entities;
using HealthDesk.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(CreateUserRequest request);
        Task<bool> UpdateAsync(int id, UpdateUserRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
