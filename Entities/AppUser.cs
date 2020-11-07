using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
