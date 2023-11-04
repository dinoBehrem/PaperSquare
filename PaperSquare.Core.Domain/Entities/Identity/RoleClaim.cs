using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.Identity;

public class RoleClaim : IdentityRoleClaim<string>
{
    public Role Role { get; set; }
}
