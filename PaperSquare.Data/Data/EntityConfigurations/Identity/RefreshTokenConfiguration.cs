using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Identity;

namespace PaperSquare.Data.Data.EntityConfigurations.Identity;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(token => token.Id);

        builder.Property(token => token.CreatedOnUtc).IsRequired();
        builder.Property(token => token.Expires).IsRequired();
        builder.Property(token => token.IsValid).IsRequired().HasDefaultValue(false);

        builder.HasOne(token => token.User).WithMany(user => user.RefreshTokens).HasForeignKey(token => token.UserId).IsRequired();
    }
}
