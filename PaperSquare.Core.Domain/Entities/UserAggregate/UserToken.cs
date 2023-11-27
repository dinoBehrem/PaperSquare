using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public class UserToken : IdentityUserToken<string>
{
    public User User { get; set; }
}
