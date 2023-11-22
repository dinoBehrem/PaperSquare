using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Identity;

namespace PaperSquare.Data.Data.EntityConfigurations.Identity;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable("UserTokens");

        builder.HasOne(userToken => userToken.User).WithMany(User => User.Tokens).HasForeignKey(userToken => userToken.UserId).IsRequired();
    }
}
