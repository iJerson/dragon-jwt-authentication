using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonOAuth.Models
{
    public class User
    {
        public long UserId { get; set; }
        public IList<UserRole> Roles { get; set; }
    }

    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}