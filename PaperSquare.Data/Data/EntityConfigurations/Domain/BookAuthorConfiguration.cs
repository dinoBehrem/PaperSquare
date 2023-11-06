using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasKey(ba => new { ba.AuthorId, ba.BookId });

            builder.HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ba => ba.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
