using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookGenreConfiguration : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder.HasKey(bg => new { bg.GenreId, bg.BookId });

            builder.HasOne(bg => bg.Book)
                .WithMany(b => b.Genres)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(bg => bg.Genre)
                .WithMany(b => b.Books)
                .HasForeignKey(bg => bg.GenreId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
