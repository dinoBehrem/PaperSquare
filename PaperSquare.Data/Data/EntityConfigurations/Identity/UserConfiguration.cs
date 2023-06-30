using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Data.Generators;

namespace PaperSquare.Data.Data.EntityConfigurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(user => user.Firstname).IsRequired();
            builder.Property(user => user.Lastname).IsRequired();
            builder.Property(user => user.BirthDate).IsRequired();
            builder.Property(user => user.Email).IsRequired();

            builder.HasMany(user => user.Claims).WithOne(claim => claim.User).HasForeignKey(userClaim => userClaim.UserId).IsRequired();
            builder.HasMany(user => user.Logins).WithOne(login => login.User).HasForeignKey(userLogin => userLogin.UserId).IsRequired();
            builder.HasMany(user => user.Roles).WithOne(role => role.User).HasForeignKey(userRole => userRole.UserId).IsRequired();
            builder.HasMany(user => user.Tokens).WithOne(token => token.User).HasForeignKey(userToken => userToken.UserId).IsRequired();
            builder.HasMany(user => user.RefreshTokens).WithOne(token => token.User).HasForeignKey(userToken => userToken.UserId).IsRequired();
        }
    }
}
