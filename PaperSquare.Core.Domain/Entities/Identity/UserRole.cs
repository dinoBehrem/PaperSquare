using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.Identity;

public class UserRole : IdentityUserRole<string>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
