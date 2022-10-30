using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Models.Identity
{
    public class RoleClaim: IdentityRoleClaim<string>
    {
        public Role Role { get; set; }
    }
}
