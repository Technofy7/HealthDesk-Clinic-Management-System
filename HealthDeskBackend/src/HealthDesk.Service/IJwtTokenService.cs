using HealthDesk.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
