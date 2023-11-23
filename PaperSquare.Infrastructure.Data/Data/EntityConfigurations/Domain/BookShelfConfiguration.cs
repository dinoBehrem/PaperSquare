using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class BookShelfConfiguration : IEntityTypeConfiguration<BookShelf>
{
    public void Configure(EntityTypeBuilder<BookShelf> builder)
    {
        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.Name).IsRequired();

        builder.HasOne(bs => bs.User)
            .WithMany(u => u.Shelves)
            .HasForeignKey(bs => bs.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Books)
            .WithOne(bis => bis.BookShelf)
            .HasForeignKey(bis => bis.BookShelfId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
