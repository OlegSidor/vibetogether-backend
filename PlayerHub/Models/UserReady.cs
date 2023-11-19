using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerHub.Models
{
    public class UserReady
    {
        public string userId { get; set; }
        public bool isReady { get; set; } = false;
    }
}
