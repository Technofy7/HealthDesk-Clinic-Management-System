using HealthDesk.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Name).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
            builder.Property(u => u.Roles).HasMaxLength(20).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
