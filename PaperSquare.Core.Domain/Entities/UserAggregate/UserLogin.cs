using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.UserAggregate;

public class UserLogin : IdentityUserLogin<string>
{
    public User User { get; set; }
}
