using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Domain.Entities.Identity
{
    public class UserToken : IdentityUserToken<string>
    {
        public User User { get; set; }
    }
}
