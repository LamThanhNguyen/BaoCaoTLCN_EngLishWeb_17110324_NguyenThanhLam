using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Models
{
    // User kế thừa IdentityUser<int>
    public class User : IdentityUser<int>
    {
        public string City { get; set; }
        public string Country { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
