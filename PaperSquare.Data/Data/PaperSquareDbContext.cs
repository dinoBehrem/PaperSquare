using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Data.Generators;
using System.Reflection;

namespace PaperSquare.Data.Data
{
    public class PaperSquareDbContext: IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public PaperSquareDbContext(DbContextOptions<PaperSquareDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
