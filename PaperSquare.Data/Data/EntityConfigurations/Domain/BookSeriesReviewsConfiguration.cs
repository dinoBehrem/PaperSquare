using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Domain.Entities.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookSeriesReviewsConfiguration : IEntityTypeConfiguration<BookSeriesReviews>
    {
        public void Configure(EntityTypeBuilder<BookSeriesReviews> builder)
        {
            builder.HasKey(bsr => new { bsr.UserId, bsr.BookSeriesId });

            builder.Property(bsr => bsr.Rating).IsRequired();
            builder.Property(bsr => bsr.Comment).IsRequired(false);

            builder.HasOne(bsr => bsr.User)
                   .WithMany(u => u.BookSeriesReviews)
                   .HasForeignKey(bsr => bsr.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(bsr => bsr.BookSeries)
                   .WithMany(bs => bs.Reviews)
                   .HasForeignKey(bsr => bsr.BookSeriesId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
