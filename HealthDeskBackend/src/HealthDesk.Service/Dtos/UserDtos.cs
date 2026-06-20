using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service.Dtos
{
    public record CreateUserRequest(string Name, string Email, string Password, string Roles);
    public record UpdateUserRequest(string Name, string Email, string Roles);
}
