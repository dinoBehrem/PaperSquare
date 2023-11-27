using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public class UserClaim : IdentityUserClaim<string>
{
    public User User { get; set; }
}
