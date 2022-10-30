using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Identity
{
    public class Role: IdentityRole
    {
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<RoleClaim> Claims { get; set; }
    }
}
