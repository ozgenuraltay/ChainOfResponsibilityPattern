using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.DAL.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
    }
}
