using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Models
{
    // Table UserRole chứa User và Role của User đó.
    public class UserRole : IdentityUserRole<int>
    {
        // 
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
