using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Domain.Entities.UserAggregate;

namespace PaperSquare.Core.Domain.Entities.Domain;

public class Role : IdentityRole
{
    public ICollection<UserRole> Roles { get; set; }
    public ICollection<RoleClaim> Claims { get; set; }
}
