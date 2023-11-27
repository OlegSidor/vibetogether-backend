using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeTogether.Authorization.Models;

namespace VibeTogether.Authorization.JWT
{
    public interface IJwtHelper
    {
        string GenerateJwtToken(VibeUser user);
    }
}
