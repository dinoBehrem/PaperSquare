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
        public static readonly string UserName = nameof(UserName);
        public static readonly string Email = nameof(Email);
        public static readonly string Id = nameof(Id);
        public static readonly string Role = nameof(Role);
    }
}
