using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain;

public class BookSeriesConfiguration : IEntityTypeConfiguration<BookSeries>
{
    public void Configure(EntityTypeBuilder<BookSeries> builder)
    {
        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.Name).IsRequired();

        builder.HasOne(bs => bs.Author)
            .WithMany(a => a.BookSeries)
            .HasForeignKey(bs => bs.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(bs => bs.Books)
            .WithOne(b => b.Series)
            .HasForeignKey(b => b.SeriesId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(bs => bs.Followers)
            .WithOne(bsf => bsf.BookSeries)
            .HasForeignKey(bsf => bsf.BookSeriesId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(bs => bs.Reviews)
            .WithOne(bsr => bsr.BookSeries)
            .HasForeignKey(bsr => bsr.BookSeriesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
