using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public class UserRole : IdentityUserRole<string>
{
    public User User { get; set; }
    public Role Role { get; set; }
}
