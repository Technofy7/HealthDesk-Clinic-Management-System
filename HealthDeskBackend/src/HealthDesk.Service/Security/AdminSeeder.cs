using HealthDesk.Data;
using HealthDesk.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HealthDesk.Service.Security
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(HealthDeskDbContext context, IConfiguration configuration)
        {
            var adminExists = await context.Users.AnyAsync(u => u.Roles == "Admin");
            if (adminExists) return;

            var name = configuration["AdminSeed:Name"] ?? "DarkArt";
            var email = configuration["AdminSeed:Email"] ?? "DarkArt6969@gmail.com";
            var password = configuration["AdminSeed:Password"] ?? "DarkArtXYZ123@909090";

            var admin = new User
            {
                Name = name,
                Email = email,
                PasswordHash = PasswordHasher.Hash(password),
                Roles = "Admin"
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
