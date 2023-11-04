using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.Identity;

public class UserToken : IdentityUserToken<string>
{
    public User User { get; set; }
}
