using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;

namespace HealthDesk.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
