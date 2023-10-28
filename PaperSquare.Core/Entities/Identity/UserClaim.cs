using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Domain.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public User User { get; set; }
    }
}
