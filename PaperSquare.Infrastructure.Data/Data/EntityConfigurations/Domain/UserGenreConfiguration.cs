using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class UserGenreConfiguration : IEntityTypeConfiguration<UserGenre>
{
    public void Configure(EntityTypeBuilder<UserGenre> builder)
    {
        builder.HasKey(ug => new { ug.UserId, ug.GenreId });

        builder.HasOne(ug => ug.User)
            .WithMany(u => u.Genres)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ug => ug.Genre)
            .WithMany(g => g.Users)
            .HasForeignKey(ug => ug.GenreId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
