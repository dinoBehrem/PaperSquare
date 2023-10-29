using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> Roles { get; set; }
        public ICollection<RoleClaim> Claims { get; set; }
    }
}
