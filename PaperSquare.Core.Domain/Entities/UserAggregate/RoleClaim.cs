using Microsoft.AspNetCore.Identity;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public class RoleClaim : IdentityRoleClaim<string>
{
    public Role Role { get; set; }
}
