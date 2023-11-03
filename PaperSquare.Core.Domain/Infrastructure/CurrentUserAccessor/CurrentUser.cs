using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Core.Infrastructure.CurrentUserAccessor
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CurrentUser(ICurrentPrincipalAccessor principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        public string UserName => _principalAccessor.Principal.GetUsername();

        public string Id => _principalAccessor.Principal.GetId();

        public string Email => _principalAccessor.Principal.GetEmail();

        public string[] Roles => _principalAccessor.Principal.GetRoles() ?? Array.Empty<string>();

        public Claim FindClaim(string claimType) => _principalAccessor.Principal?.FindFirst(claimType);

        public Claim[] FindClaims(string claimType) => _principalAccessor.Principal?.FindAll(claimType).ToArray() ?? Array.Empty<Claim>();        

        public Claim[] GetAllClaims() => _principalAccessor.Principal?.Claims.ToArray() ?? Array.Empty<Claim>();

        public bool IsInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool IsInRoleAny(params string[] roleNames)
        {
            throw new NotImplementedException();
        }
    }
}
