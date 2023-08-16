using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaperSquare.Core.Models.Domain;

namespace PaperSquare.Data.Data.EntityConfigurations.Domain
{
    public class BookReviewsConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> builder)
        {
            builder.HasKey(br => new { br.UserId, br.BookId });

            builder.Property(br => br.Rating).IsRequired();
            builder.Property(br => br.Comment).IsRequired(false);

            builder.HasOne(br => br.User)
                   .WithMany(u => u.BookReviews)
                   .HasForeignKey(br => br.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(br => br.Book)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(br => br.BookId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
