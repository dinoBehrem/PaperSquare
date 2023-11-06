using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PaperSquare.Core.Permissions;

namespace PaperSquare.Core.Infrastructure.CurrentUserAccessor
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal principal) => principal.FindFirstValue(AppClaimTypes.UserName);
        public static string GetEmail(this ClaimsPrincipal principal) => principal.FindFirstValue(AppClaimTypes.Email);
        public static string GetId(this ClaimsPrincipal principal) => principal.FindFirstValue(AppClaimTypes.Id);
        public static string[] GetRoles(this ClaimsPrincipal principal) => principal.FindAll(AppClaimTypes.Role).Select(c => c.Value).ToArray();
    }
}
