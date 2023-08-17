using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookInShelfConfiguration : IEntityTypeConfiguration<BookInShelf>
    {
        public void Configure(EntityTypeBuilder<BookInShelf> builder)
        {
            builder.HasKey(bis => new { bis.BookId, bis.BookShelfId });

            builder.Property(bis => bis.Status).IsRequired();
            builder.Property(bis => bis.Progress).IsRequired(false);

            builder.HasOne(bis => bis.BookShelf)
                .WithMany(bs => bs.Books)
                .HasForeignKey(bis  => bis.BookShelfId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(bis => bis.Book)
                .WithMany(b => b.BookShelves)
                .HasForeignKey(bis  => bis.BookId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
