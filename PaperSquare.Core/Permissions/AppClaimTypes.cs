using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Permissions
{
    public class AppClaimTypes
    {
        public static readonly string UserName = ClaimTypes.Name;
        public static readonly string Email = ClaimTypes.Email;
        public static readonly string Id = ClaimTypes.NameIdentifier;
        public static readonly string Role = ClaimTypes.Role;
    }
}
