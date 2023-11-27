using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerHub.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsReady { get; set; } = false;

        public override int GetHashCode() => UserId.GetHashCode();
        public override bool Equals(object other) => (other as User)?.UserId == UserId;
    }
}
