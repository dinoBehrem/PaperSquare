using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Core.Domain.Entities.Identity;

public class UserLogin : IdentityUserLogin<string>
{
    public User User { get; set; }
}
