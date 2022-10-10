using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Indentity
{
    public class ApplicationRole :IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

}