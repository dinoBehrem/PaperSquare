using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Domain.Entities.Identity
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public Role Role { get; set; }
    }
}
