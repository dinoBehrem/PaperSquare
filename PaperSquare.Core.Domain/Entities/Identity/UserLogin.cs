using Microsoft.AspNetCore.Identity;

namespace PaperSquare.Domain.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public User User { get; set; }
    }
}
